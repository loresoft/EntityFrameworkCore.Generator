using System.Diagnostics.CodeAnalysis;
using System.Text;

using EntityFrameworkCore.Generator.Metadata.Generation;

namespace EntityFrameworkCore.Generator.Extensions;

public static class GenerationExtensions
{
    #region Data
    private static readonly HashSet<string> _csharpKeywords = new(StringComparer.Ordinal)
    {
        "as", "do", "if", "in", "is",
        "for", "int", "new", "out", "ref", "try",
        "base", "bool", "byte", "case", "char", "else", "enum", "goto", "lock", "long", "null", "this", "true", "uint", "void",
        "break", "catch", "class", "const", "event", "false", "fixed", "float", "sbyte", "short", "throw", "ulong", "using", "while",
        "double", "extern", "object", "params", "public", "return", "sealed", "sizeof", "static", "string", "struct", "switch", "typeof", "unsafe", "ushort",
        "checked", "decimal", "default", "finally", "foreach", "private", "virtual",
        "abstract", "continue", "delegate", "explicit", "implicit", "internal", "operator", "override", "readonly", "volatile",
        "__arglist", "__makeref", "__reftype", "interface", "namespace", "protected", "unchecked",
        "__refvalue", "stackalloc"
    };

    private static readonly HashSet<string> _defaultNamespaces =
    [
        "System",
        "System.Collections.Generic",
    ];

    private static readonly Dictionary<Type, string> _csharpTypeAlias = new(16)
    {
        { typeof(bool), "bool" },
        { typeof(byte), "byte" },
        { typeof(char), "char" },
        { typeof(decimal), "decimal" },
        { typeof(double), "double" },
        { typeof(float), "float" },
        { typeof(int), "int" },
        { typeof(long), "long" },
        { typeof(object), "object" },
        { typeof(sbyte), "sbyte" },
        { typeof(short), "short" },
        { typeof(string), "string" },
        { typeof(uint), "uint" },
        { typeof(ulong), "ulong" },
        { typeof(ushort), "ushort" },
        { typeof(void), "void" }
    };
    #endregion

    public static string ToFieldName(this string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        return "_" + name.ToCamelCase();
    }

    public static string MakeUnique(this string name, Func<string, bool> exists)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(exists);

        string uniqueName = name;
        int count = 1;

        while (exists(uniqueName))
            uniqueName = string.Concat(name, count++);

        return uniqueName;
    }

    public static bool IsKeyword(this string text)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);

        return _csharpKeywords.Contains(text);
    }

    [return: NotNullIfNotNull(nameof(name))]
    public static string? ToSafeName(this string? name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        if (!name.IsKeyword())
            return name;

        return "@" + name;
    }

    public static string ToType(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var stringBuilder = new StringBuilder();
        ProcessType(stringBuilder, type);
        return stringBuilder.ToString();
    }

    public static string? ToNullableType(this Type type, bool isNullable = false)
    {
        bool isValueType = type.IsValueType;

        var typeString = type.ToType();

        if (!isValueType || !isNullable)
            return typeString;

        return typeString.EndsWith('?') ? typeString : typeString + "?";
    }

    public static bool IsValueType(this string? type)
    {
        if (string.IsNullOrEmpty(type))
            return false;

        if (!type.StartsWith("System."))
            return false;

        var t = Type.GetType(type, false);
        return t != null && t.IsValueType;
    }

    public static string ToLiteral(this string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        return value.Contains('\n') || value.Contains('\r')
            ? "@\"" + value.Replace("\"", "\"\"") + "\""
            : "\"" + value.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
    }



    private static void ProcessType(StringBuilder builder, Type type)
    {
        if (type.IsGenericType)
        {
            var genericArguments = type.GetGenericArguments();
            ProcessGenericType(builder, type, genericArguments, genericArguments.Length);
        }
        else if (type.IsArray)
        {
            ProcessArrayType(builder, type);
        }
        else if (_csharpTypeAlias.TryGetValue(type, out var builtInName))
        {
            builder.Append(builtInName);
        }
        else if (type.Namespace.HasValue() && _defaultNamespaces.Contains(type.Namespace))
        {
            builder.Append(type.Name);
        }
        else
        {
            builder.Append(type.FullName ?? type.Name);
        }
    }

    private static void ProcessArrayType(StringBuilder builder, Type type)
    {
        var innerType = type;
        while (innerType.IsArray)
        {
            innerType = innerType.GetElementType()!;
        }

        ProcessType(builder, innerType);

        while (type.IsArray)
        {
            builder.Append('[');
            builder.Append(',', type.GetArrayRank() - 1);
            builder.Append(']');
            type = type.GetElementType()!;
        }
    }

    private static void ProcessGenericType(StringBuilder builder, Type type, Type[] genericArguments, int length)
    {
        if (type.IsConstructedGenericType
            && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            ProcessType(builder, type.GetUnderlyingType());
            builder.Append('?');
            return;
        }

        var offset = type.DeclaringType != null ? type.DeclaringType.GetGenericArguments().Length : 0;
        var genericPartIndex = type.Name.IndexOf('`');
        if (genericPartIndex <= 0)
        {
            builder.Append(type.Name);
            return;
        }

        builder.Append(type.Name, 0, genericPartIndex);
        builder.Append('<');

        for (var i = offset; i < length; i++)
        {
            ProcessType(builder, genericArguments[i]);
            if (i + 1 == length)
            {
                continue;
            }

            builder.Append(',');
            if (!genericArguments[i + 1].IsGenericParameter)
            {
                builder.Append(' ');
            }
        }

        builder.Append('>');
    }
}
