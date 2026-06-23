using System.Text;

using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Core.Tests.Templates;

public class TypescriptScript
{
    // simulate global script variables
    public TemplateOptions TemplateOptions { get; set; } = null!;

    public GeneratorOptions GeneratorOptions { get; set; } = null!;

    public IndentedStringBuilder CodeBuilder { get; set; } = null!;

    public Model Model { get; set; } = null!;



    public string WriteCode()
    {
        CodeBuilder.Clear();

        var hasNamespace = TemplateOptions.Namespace.HasValue();
        if (hasNamespace)
        {
            CodeBuilder
                .Append("namespace ")
                .Append(TemplateOptions.Namespace)
                .AppendLine(" {");

            CodeBuilder.IncrementIndent();
        }

        GenerateInterface();

        if (hasNamespace)
        {
            CodeBuilder.DecrementIndent();
            CodeBuilder.AppendLine("}");
        }

        return CodeBuilder.ToString();
    }

    private void GenerateInterface()
    {
        var modelClass = Model.ModelClass.ToSafeName().ToCamelCase();

        CodeBuilder.Append($"export interface {modelClass}");

        if (TemplateOptions.BaseClass.HasValue())
        {
            var modelBase = TemplateOptions.BaseClass.ToSafeName().ToCamelCase();
            CodeBuilder.Append($" extends {modelBase}");
        }

        CodeBuilder.AppendLine(" {");

        using (CodeBuilder.Indent())
            GenerateProperties();

        CodeBuilder.AppendLine("}");
    }

    private void GenerateProperties()
    {
        foreach (var property in Model.Properties)
        {
            var propertyType = ToScriptType(property.SystemType);
            var propertyName = property.PropertyName.ToSafeName().ToCamelCase();

            CodeBuilder.Append(propertyName);
            if (property.IsOptional)
                CodeBuilder.Append("?");

            CodeBuilder
                .Append(": ")
                .Append(propertyType)
                .AppendLine(";");
        }
    }

    private static string ToScriptType(Type type)
    {
        var t = type.FullName;

        return t switch
        {
            "System.Int16" or "System.Int32" or "System.Byte" or "System.Double" or "System.SByte" or "System.Single" or "System.UInt16" or "System.UInt32" => "number",
            "System.Decimal" or "System.Int64" or "System.UInt64" => "number",
            "System.Boolean" => "boolean",
            "System.DateTime" or "System.DateTimeOffset" => "Date",
            "System.String" or "System.Guid" or "System.TimeSpan" => "string",
            _ => "any",
        };
    }
}
