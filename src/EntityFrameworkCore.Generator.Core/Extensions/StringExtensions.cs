using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EntityFrameworkCore.Generator.Metadata.Generation;

namespace EntityFrameworkCore.Generator.Extensions
{
    public static class StringExtensions
    {
        #region Data
        private static readonly HashSet<string> _csharpKeywords = new HashSet<string>(StringComparer.Ordinal)
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

        private static readonly HashSet<string> _visualBasicKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "as", "do", "if", "in", "is", "me", "of", "on", "or", "to",
            "and", "dim", "end", "for", "get", "let", "lib", "mod", "new", "not", "rem", "set", "sub", "try", "xor",
            "ansi", "auto", "byte", "call", "case", "cdbl", "cdec", "char", "cint", "clng", "cobj", "csng", "cstr", "date", "each", "else",
            "enum", "exit", "goto", "like", "long", "loop", "next", "step", "stop", "then", "true", "wend", "when", "with",
            "alias", "byref", "byval", "catch", "cbool", "cbyte", "cchar", "cdate", "class", "const", "ctype", "cuint", "culng", "endif", "erase", "error",
            "event", "false", "gosub", "isnot", "redim", "sbyte", "short", "throw", "ulong", "until", "using", "while",
            "csbyte", "cshort", "double", "elseif", "friend", "global", "module", "mybase", "object", "option", "orelse", "public", "resume", "return", "select", "shared",
            "single", "static", "string", "typeof", "ushort",
            "andalso", "boolean", "cushort", "decimal", "declare", "default", "finally", "gettype", "handles", "imports", "integer", "myclass", "nothing", "partial", "private", "shadows",
            "trycast", "unicode", "variant",
            "assembly", "continue", "delegate", "function", "inherits", "operator", "optional", "preserve", "property", "readonly", "synclock", "uinteger", "widening",
            "addressof", "interface", "namespace", "narrowing", "overloads", "overrides", "protected", "structure", "writeonly",
            "addhandler", "directcast", "implements", "paramarray", "raiseevent", "withevents",
            "mustinherit", "overridable",
            "mustoverride",
            "removehandler",
            "class_finalize", "notinheritable", "notoverridable",
            "class_initialize"
        };

        private static readonly Dictionary<string, string> _csharpTypeAlias = new Dictionary<string, string>(16)
        {
            {"System.Int16", "short"},
            {"System.Int32", "int"},
            {"System.Int64", "long"},
            {"System.String", "string"},
            {"System.Object", "object"},
            {"System.Boolean", "bool"},
            {"System.Void", "void"},
            {"System.Char", "char"},
            {"System.Byte", "byte"},
            {"System.UInt16", "ushort"},
            {"System.UInt32", "uint"},
            {"System.UInt64", "ulong"},
            {"System.SByte", "sbyte"},
            {"System.Single", "float"},
            {"System.Double", "double"},
            {"System.Decimal", "decimal"}
        };
        #endregion

        private static readonly Regex _splitNameRegex = new Regex(@"[\W_]+");


        /// <summary>
        /// Indicates whether the specified String object is null or an empty string
        /// </summary>
        /// <param name="item">A String reference</param>
        /// <returns>
        ///     <c>true</c> if is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters
        /// </summary>
        /// <param name="item">A String reference</param>
        /// <returns>
        ///      <c>true</c> if is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string item)
        {
            if (item == null)
                return true;

            for (int i = 0; i < item.Length; i++)
                if (!char.IsWhiteSpace(item[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Determines whether the specified string is not <see cref="IsNullOrEmpty"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="value"/> is not <see cref="IsNullOrEmpty"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Does string contain both uppercase and lowercase characters?
        /// </summary>
        /// <param name="s">The value.</param>
        /// <returns>True if contain mixed case.</returns>
        public static bool IsMixedCase(this string s)
        {
            if (s.IsNullOrEmpty())
                return false;

            var containsUpper = s.Any(Char.IsUpper);
            var containsLower = s.Any(Char.IsLower);

            return containsLower && containsUpper;
        }

        /// <summary>
        /// Converts a string to use camelCase.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The to camel case. </returns>
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            string output = ToPascalCase(value);
            if (output.Length > 2)
                return char.ToLower(output[0]) + output.Substring(1);

            return output.ToLower();
        }

        /// <summary>
        /// Converts a string to use PascalCase.
        /// </summary>
        /// <param name="value">Text to convert</param>
        /// <returns>The string</returns>
        public static string ToPascalCase(this string value)
        {
            return value.ToPascalCase(_splitNameRegex);
        }

        /// <summary>
        /// Converts a string to use PascalCase.
        /// </summary>
        /// <param name="value">Text to convert</param>
        /// <param name="splitRegex">Regular Expression to split words on.</param>
        /// <returns>The string</returns>
        public static string ToPascalCase(this string value, Regex splitRegex)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var mixedCase = value.IsMixedCase();
            var names = splitRegex.Split(value);
            var output = new StringBuilder();

            if (names.Length > 1)
            {
                foreach (string name in names)
                {
                    if (name.Length > 1)
                    {
                        output.Append(char.ToUpper(name[0]));
                        output.Append(mixedCase ? name.Substring(1) : name.Substring(1).ToLower());
                    }
                    else
                    {
                        output.Append(name);
                    }
                }
            }
            else if (value.Length > 1)
            {
                output.Append(char.ToUpper(value[0]));
                output.Append(mixedCase ? value.Substring(1) : value.Substring(1).ToLower());
            }
            else
            {
                output.Append(value.ToUpper());
            }

            return output.ToString();
        }




        public static string ToFieldName(this string name)
        {
            return "_" + ToCamelCase(name);
        }

        public static string MakeUnique(this string name, Func<string, bool> exists)
        {
            string uniqueName = name;
            int count = 1;

            while (exists(uniqueName))
                uniqueName = string.Concat(name, count++);

            return uniqueName;
        }

        public static bool IsKeyword(this string text, CodeLanguage language = CodeLanguage.CSharp)
        {
            return language == CodeLanguage.VisualBasic
              ? _visualBasicKeywords.Contains(text)
              : _csharpKeywords.Contains(text);
        }

        public static string ToSafeName(this string name, CodeLanguage language = CodeLanguage.CSharp)
        {
            if (!name.IsKeyword(language))
                return name;

            return language == CodeLanguage.VisualBasic
              ? string.Format("[{0}]", name)
              : "@" + name;
        }

        public static string ToType(this Type type, CodeLanguage language = CodeLanguage.CSharp)
        {
            return ToType(type.FullName, language);
        }

        public static string ToType(this string type, CodeLanguage language = CodeLanguage.CSharp)
        {
            if (type == "System.Xml.XmlDocument")
                type = "System.String";

            string t;
            if (language == CodeLanguage.CSharp && _csharpTypeAlias.TryGetValue(type, out t))
                return t;

            // drop system from namespace
            string[] parts = type.Split('.');
            if (parts.Length == 2 && parts[0] == "System")
                return parts[1];

            return type;
        }

        public static string ToNullableType(this Type type, bool isNullable = false, CodeLanguage language = CodeLanguage.CSharp)
        {
            return ToNullableType(type.FullName, isNullable, language);
        }

        public static string ToNullableType(this string type, bool isNullable = false, CodeLanguage language = CodeLanguage.CSharp)
        {
            bool isValueType = type.IsValueType();

            type = type.ToType(language);

            if (!isValueType || !isNullable)
                return type;

            return language == CodeLanguage.VisualBasic
              ? string.Format("Nullable(Of {0})", type)
              : type + "?";
        }

        public static bool IsValueType(this string type)
        {
            if (!type.StartsWith("System."))
                return false;

            var t = Type.GetType(type, false);
            return t != null && t.IsValueType;
        }

    }
}
