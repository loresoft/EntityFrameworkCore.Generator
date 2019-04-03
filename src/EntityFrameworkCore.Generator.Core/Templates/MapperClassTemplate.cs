using System.Collections.Generic;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
    public class MapperClassTemplate : CodeTemplateBase
    {
        private readonly Entity _entity;

        public MapperClassTemplate(Entity entity, GeneratorOptions options) : base(options)
        {
            _entity = entity;
        }

        public override string WriteCode()
        {
            CodeBuilder.Clear();

            CodeBuilder.AppendLine("using System;");
            CodeBuilder.AppendLine("using AutoMapper;");

            var imports = new SortedSet<string>();
            imports.Add(_entity.EntityNamespace);

            foreach (var model in _entity.Models)
                imports.Add(model.ModelNamespace);

            foreach (var import in imports)
                if (_entity.MapperNamespace != import)
                    CodeBuilder.AppendLine($"using {import};");

            CodeBuilder.AppendLine();

            CodeBuilder.AppendLine($"namespace {_entity.MapperNamespace}");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateClass();
            }

            CodeBuilder.AppendLine("}");

            return CodeBuilder.ToString();
        }

        private void GenerateClass()
        {
            var entityClass = _entity.EntityClass.ToSafeName();
            var mapperClass = _entity.MapperClass.ToSafeName();

            if (Options.Model.Mapper.Document)
            {
                CodeBuilder.AppendLine("/// <summary>");
                CodeBuilder.AppendLine($"/// Mapper class for entity <see cref=\"{entityClass}\"/> .");
                CodeBuilder.AppendLine("/// </summary>");
            }

            CodeBuilder.AppendLine($"public partial class {mapperClass}");

            if (_entity.MapperBaseClass.HasValue())
            {
                var mapperBaseClass = _entity.MapperBaseClass.ToSafeName();
                using (CodeBuilder.Indent())
                    CodeBuilder.AppendLine($": {mapperBaseClass}");
            }

            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateConstructor();

                if (Options.Model.Mapper.Document)
                {
                    CodeBuilder.AppendLine("/// <summary>");
                    CodeBuilder.AppendLine($"/// Partial method that can be implemented in a partial class to initialize custom mapping.");
                    CodeBuilder.AppendLine("/// </summary>");
                }
                CodeBuilder.AppendLine("partial void CreateCustomMap();");
            }

            CodeBuilder.AppendLine("}");
        }

        private void GenerateConstructor()
        {
            var mapperClass = _entity.MapperClass.ToSafeName();

            var entityClass = _entity.EntityClass.ToSafeName();
            var entityFullName = $"{_entity.EntityNamespace}.{entityClass}";

            if (Options.Model.Mapper.Document)
            {
                CodeBuilder.AppendLine("/// <summary>");
                CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{mapperClass}\"/> class.");
                CodeBuilder.AppendLine("/// </summary>");
            }

            CodeBuilder.AppendLine($"public {mapperClass}()");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                foreach (var model in _entity.Models)
                {
                    var modelClass = model.ModelClass.ToSafeName();
                    var modelFullName = $"{model.ModelNamespace}.{modelClass}";

                    switch (model.ModelType)
                    {
                        case ModelType.Read:
                            CodeBuilder.AppendLine($"CreateMap<{entityFullName}, {modelFullName}>();");
                            break;
                        case ModelType.Create:
                            CodeBuilder.AppendLine($"CreateMap<{modelFullName}, {entityFullName}>();");
                            break;
                        case ModelType.Update:
                            CodeBuilder.AppendLine($"CreateMap<{entityFullName}, {modelFullName}>();");
                            CodeBuilder.AppendLine($"CreateMap<{modelFullName}, {entityFullName}>();");
                            break;
                    }
                }

                CodeBuilder.AppendLine("CreateCustomMap();");
            }

            CodeBuilder.AppendLine("}");
            CodeBuilder.AppendLine();
        }

    }
}
