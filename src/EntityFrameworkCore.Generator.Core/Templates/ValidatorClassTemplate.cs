using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates;

public class ValidatorClassTemplate : CodeTemplateBase
{
    private readonly Model _model;

    public ValidatorClassTemplate(Model model, GeneratorOptions options) : base(options)
    {
        _model = model;
    }

    public override string WriteCode()
    {
        CodeBuilder.Clear();

        if (Options.Model.Validator.Header.HasValue())
            CodeBuilder.AppendLine(Options.Model.Validator.Header).AppendLine();

        CodeBuilder.AppendLine("using System;");
        CodeBuilder.AppendLine();
        CodeBuilder.AppendLine("using FluentValidation;");

        if (_model.ModelNamespace != _model.ValidatorNamespace)
            CodeBuilder.AppendLine($"using {_model.ModelNamespace};");

        CodeBuilder.AppendLine();

        CodeBuilder.Append($"namespace {_model.ValidatorNamespace}");

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
        var validatorClass = _model.ValidatorClass.ToSafeName();
        var modelClass = _model.ModelClass.ToSafeName();
        var modelFullName = $"{_model.ModelNamespace}.{modelClass}";

        if (Options.Model.Validator.Document)
        {
            GenerateClassDocumentation(modelFullName);
        }
        if (Options.Model.Validator.Attributes.HasValue())
        {
            CodeBuilder.AppendLine(Options.Model.Validator.Attributes);
        }
        CodeBuilder.AppendLine($"public partial class {validatorClass}");

        if (_model.ValidatorBaseClass.HasValue())
        {
            var validatorBase = _model.ValidatorBaseClass.ToSafeName();
            using (CodeBuilder.Indent())
                CodeBuilder.AppendLine($": {validatorBase}");
        }

        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            GenerateConstructor();
        }

        CodeBuilder.AppendLine("}");
    }

    private void GenerateConstructor()
    {
        var validatorClass = _model.ValidatorClass.ToSafeName();
        var validatorFullName = $"{_model.ValidatorNamespace}.{validatorClass}";
        var modelClass = _model.ModelClass.ToSafeName();
        var modelFullName = $"{_model.ModelNamespace}.{modelClass}";

        if (Options.Model.Validator.Document)
        {
            GenerateConstructorDocumentation(validatorFullName, modelFullName);
        }

        CodeBuilder.AppendLine($"public {validatorClass}()");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            CodeBuilder.AppendLine("#region Generated Constructor");
            foreach (var property in _model.Properties)
            {
                if (property.IsComputed == true || property.IsIdentity == true || property.IsRowVersion == true)
                    continue;

                var propertyName = property.PropertyName.ToSafeName();

                if (property.IsRequired && property.SystemType == typeof(string))
                    CodeBuilder.AppendLine($"RuleFor(p => p.{propertyName}).NotEmpty();");

                if (property.Size.HasValue && property.SystemType == typeof(string) && property.Size > 0)
                    CodeBuilder.AppendLine($"RuleFor(p => p.{propertyName}).MaximumLength({property.Size});");

            }
            CodeBuilder.AppendLine("#endregion");
        }

        CodeBuilder.AppendLine("}");
        CodeBuilder.AppendLine();
    }

    private void GenerateClassDocumentation(string modelClass)
    {
        var modelType = _model.ModelType switch
        {
            ModelType.Create => "create",
            ModelType.Update => "update",
            _ => "read"
        };

        var entityName = ToXmlText(_model.Entity?.EntityClass);
        var sourceName = ToXmlText(GetQualifiedTableName());
        var sourceType = _model.Entity?.IsView == true ? "view" : "table";

        CodeBuilder.AppendLine("/// <summary>");

        if (sourceName.HasValue())
            CodeBuilder.AppendLine($"/// Defines FluentValidation rules for the <see cref=\"{modelClass}\" /> {modelType} model for the <c>{entityName}</c> entity mapped to the <c>{sourceName}</c> {sourceType}.");
        else if (entityName.HasValue())
            CodeBuilder.AppendLine($"/// Defines FluentValidation rules for the <see cref=\"{modelClass}\" /> {modelType} model for the <c>{entityName}</c> entity.");
        else
            CodeBuilder.AppendLine($"/// Defines FluentValidation rules for the <see cref=\"{modelClass}\" /> {modelType} model.");

        CodeBuilder.AppendLine("/// </summary>");
    }

    private void GenerateConstructorDocumentation(string validatorClass, string modelClass)
    {
        CodeBuilder.AppendLine("/// <summary>");
        CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{validatorClass}\"/> class and configures validation rules for <see cref=\"{modelClass}\" />.");
        CodeBuilder.AppendLine("/// </summary>");
    }

    private string? GetQualifiedTableName()
    {
        if (_model.Entity?.TableName.IsNullOrEmpty() != false)
            return _model.Entity?.TableName;

        return _model.Entity.TableSchema.HasValue()
            ? $"{_model.Entity.TableSchema}.{_model.Entity.TableName}"
            : _model.Entity.TableName;
    }

}
