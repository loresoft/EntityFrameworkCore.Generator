using System;
using EntityFrameworkCore.Generator.Metadata.Generation;
using System.Collections.Generic;
using EntityFrameworkCore.Generator.Extensions;

namespace EntityFrameworkCore.Generator.Templates
{
    public class MapperClassTemplate : CodeTemplateBase
    {
        private readonly Entity _entity;

        public MapperClassTemplate(Entity entity)
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

            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Mapper class for entity <see cref=\"{entityClass}\"/> .");
            CodeBuilder.AppendLine("/// </summary>");

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
            }

            CodeBuilder.AppendLine("}");
        }

        private void GenerateConstructor()
        {
            var mapperClass = _entity.MapperClass.ToSafeName();
            var entityClass = _entity.EntityClass.ToSafeName();

            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{mapperClass}\"/> class.");
            CodeBuilder.AppendLine("/// </summary>");

            CodeBuilder.AppendLine($"public {mapperClass}()");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                foreach (var model in _entity.Models)
                {
                    var modelClass = model.ModelClass.ToSafeName();

                    switch (model.ModelType)
                    {
                        case ModelType.Read:
                            CodeBuilder.AppendLine($"CreateMap<{entityClass}, {modelClass}>();");
                            break;
                        case ModelType.Create:
                        case ModelType.Update:
                            CodeBuilder.AppendLine($"CreateMap<{modelClass}, {entityClass}>();");
                            break;
                    }
                }
            }

            CodeBuilder.AppendLine("}");
            CodeBuilder.AppendLine();
        }

    }
}
