using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates;

public class ModelClassTemplate : CodeTemplateBase
{
    private readonly Model _model;

    public ModelClassTemplate(Model model, GeneratorOptions options) : base(options)
    {
        _model = model;
    }

    public override string WriteCode()
    {
        CodeBuilder.Clear();

        if (_model.ModelHeader.HasValue())
            CodeBuilder.AppendLine(_model.ModelHeader).AppendLine();

        CodeBuilder.AppendLine("using System;");
        CodeBuilder.AppendLine("using System.Collections.Generic;");
        CodeBuilder.AppendLine();

        CodeBuilder.Append($"namespace {_model.ModelNamespace}");

        if (Options.Project.FileScopedNamespace)
        {
            CodeBuilder.AppendLine(";");
            CodeBuilder.AppendLine();
            GenerateClass();
        }
        else
        {
            CodeBuilder.AppendLine();
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateClass();
            }

            CodeBuilder.AppendLine("}");
        }

        return CodeBuilder.ToString();
    }

    private void GenerateClass()
    {
        var modelClass = _model.ModelClass.ToSafeName();


        if (ShouldDocument())
        {
            GenerateClassDocumentation();
        }
        if (_model.ModelAttributes.HasValue())
        {
            CodeBuilder.AppendLine(_model.ModelAttributes);
        }
        CodeBuilder.AppendLine($"public partial class {modelClass}");

        if (_model.ModelBaseClass.HasValue())
        {
            var modelBase = _model.ModelBaseClass.ToSafeName();
            using (CodeBuilder.Indent())
                CodeBuilder.AppendLine($": {modelBase}");
        }

        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            GenerateProperties();
        }

        CodeBuilder.AppendLine("}");

    }


    private void GenerateProperties()
    {
        CodeBuilder.AppendLine("#region Generated Properties");
        foreach (var property in _model.Properties)
        {
            var propertyType = GetPropertyType(property);
            var propertyName = property.PropertyName.ToSafeName();

            if (ShouldDocument())
            {
                GeneratePropertyDocumentation(property);
            }

            if (property.IsNullable == true && (property.SystemType.IsValueType || Options.Project.Nullable))
                CodeBuilder.AppendLine($"public {ToNullablePropertyType(propertyType)} {propertyName} {{ get; set; }}");
            else if (Options.Project.Nullable && !property.SystemType.IsValueType)
                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }} = null!;");
            else
                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }}");

            CodeBuilder.AppendLine();
        }
        CodeBuilder.AppendLine("#endregion");
        CodeBuilder.AppendLine();
    }

    private void GenerateClassDocumentation()
    {
        var modelType = _model.ModelType switch
        {
            ModelType.Create => "create",
            ModelType.Update => "update",
            _ => "read"
        };

        var entityName = ToXmlText(_model.Entity?.EntityClass ?? _model.ModelClass);
        var sourceName = ToXmlText(_model.Entity?.TableName);
        var sourceType = _model.Entity?.IsView == true ? "view" : "table";

        CodeBuilder.AppendLine("/// <summary>");

        if (sourceName.HasValue())
            CodeBuilder.AppendLine($"/// Represents a {modelType} model for the <c>{entityName}</c> entity mapped to the <c>{sourceName}</c> {sourceType}.");
        else
            CodeBuilder.AppendLine($"/// Represents a {modelType} model for the <c>{entityName}</c> entity.");

        CodeBuilder.AppendLine("/// </summary>");
    }

    private void GeneratePropertyDocumentation(Property property)
    {
        var propertyName = ToXmlText(property.PropertyName);
        var columnName = ToXmlText(property.ColumnName);

        CodeBuilder.AppendLine("/// <summary>");

        if (columnName.HasValue())
            CodeBuilder.AppendLine($"/// Gets or sets the <c>{propertyName}</c> value mapped from the <c>{columnName}</c> column.");
        else
            CodeBuilder.AppendLine($"/// Gets or sets the <c>{propertyName}</c> value.");

        CodeBuilder.AppendLine("/// </summary>");
        CodeBuilder.AppendLine("/// <value>");
        CodeBuilder.AppendLine($"/// The <c>{propertyName}</c> model value.");
        CodeBuilder.AppendLine("/// </value>");
    }

    private static string GetPropertyType(Property property)
    {
        return property.SystemTypeName.HasValue()
            ? property.SystemTypeName
            : property.SystemType.ToType();
    }

    private static string ToNullablePropertyType(string propertyType)
    {
        return propertyType.EndsWith('?')
            ? propertyType
            : propertyType + "?";
    }


    private bool ShouldDocument()
    {
        if (_model.ModelType == ModelType.Create)
            return Options.Model.Create.Document;

        if (_model.ModelType == ModelType.Update)
            return Options.Model.Update.Document;

        return Options.Model.Read.Document;
    }
}
