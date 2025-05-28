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

        if (Options.Model.Validator.Document)
        {
            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Validator class for <see cref=\"{modelClass}\"/> .");
            CodeBuilder.AppendLine("/// </summary>");
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

        if (Options.Model.Validator.Document)
        {
            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{validatorClass}\"/> class.");
            CodeBuilder.AppendLine("/// </summary>");
        }

        CodeBuilder.AppendLine($"public {validatorClass}()");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            CodeBuilder.AppendLine("#region Generated Constructor");
            foreach (var property in _model.Properties)
            {
                if (property.ValueGenerated.HasValue)
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

}
