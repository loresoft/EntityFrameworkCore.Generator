using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
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

            CodeBuilder.AppendLine("using System;");
            CodeBuilder.AppendLine("using System.Collections.Generic;");
            CodeBuilder.AppendLine();

            CodeBuilder.AppendLine($"namespace {_model.ModelNamespace}");
            CodeBuilder.AppendLine("{");

            using(CodeBuilder.Indent())
            {
                GenerateClass();
            }

            CodeBuilder.AppendLine("}");

            return CodeBuilder.ToString();
        }

        private void GenerateClass()
        {
            var modelClass = _model.ModelClass.ToSafeName();

            if (ShouldDocument())
            {
                CodeBuilder.AppendLine("/// <summary>");
                CodeBuilder.AppendLine("/// View Model class");
                CodeBuilder.AppendLine("/// </summary>");
            }

            CodeBuilder.AppendLine($"public partial class {modelClass}");

            if (_model.ModelBaseClass.HasValue())
            {
                var modelBase = _model.ModelBaseClass.ToSafeName();
                using(CodeBuilder.Indent())
                CodeBuilder.AppendLine($": {modelBase}");
            }

            CodeBuilder.AppendLine("{");

            using(CodeBuilder.Indent())
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
                var propertyType = property.SystemType.ToNullableType(property.IsNullable == true);
                var propertyName = property.PropertyName.ToIdentifierName(Options.Data.Entity.IdentifierNaming);

                if (ShouldDocument())
                {
                    CodeBuilder.AppendLine("/// <summary>");
                    CodeBuilder.AppendLine($"/// Gets or sets the property value for '{property.PropertyName.ToIdentifierName(Options.Data.Entity.IdentifierNaming)}'.");
                    CodeBuilder.AppendLine("/// </summary>");
                    CodeBuilder.AppendLine("/// <value>");
                    CodeBuilder.AppendLine($"/// The property value for '{property.PropertyName.ToIdentifierName(Options.Data.Entity.IdentifierNaming)}'.");
                    CodeBuilder.AppendLine("/// </value>");
                }

                propertyName = propertyName.ToSafeName();
                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }}");
                CodeBuilder.AppendLine();
            }
            CodeBuilder.AppendLine("#endregion");
            CodeBuilder.AppendLine();
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
}