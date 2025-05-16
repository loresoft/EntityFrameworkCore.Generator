using System.Collections.Generic;

using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates;

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

        var imports = new SortedSet<string>
        {
            _entity.EntityNamespace
        };

        foreach (var model in _entity.Models)
            imports.Add(model.ModelNamespace);

        foreach (var import in imports)
        {
            if (_entity.MapperNamespace != import)
                CodeBuilder.AppendLine($"using {import};");
        }

        CodeBuilder.AppendLine();

        CodeBuilder.Append($"namespace {_entity.MapperNamespace}");

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
        var entityClass = _entity.EntityClass.ToSafeName();
        var mapperClass = _entity.MapperClass.ToSafeName();

        if (Options.Model.Mapper.Document)
        {
            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Mapper class for entity <see cref=\"{entityClass}\"/> .");
            CodeBuilder.AppendLine("/// </summary>");
        }
        if (Options.Model.Mapper.Attributes.HasValue())
        {
            CodeBuilder.AppendLine(Options.Model.Mapper.Attributes);
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

        string? readFullName = null;
        string? updateFullName = null;

        using (CodeBuilder.Indent())
        {
            foreach (var model in _entity.Models)
            {
                var modelClass = model.ModelClass.ToSafeName();
                var modelFullName = $"{model.ModelNamespace}.{modelClass}";

                switch (model.ModelType)
                {
                    case ModelType.Read:
                        readFullName = modelFullName;
                        CodeBuilder.AppendLine($"CreateMap<{entityFullName}, {modelFullName}>();").AppendLine();
                        break;
                    case ModelType.Create:
                        CodeBuilder.AppendLine($"CreateMap<{modelFullName}, {entityFullName}>();").AppendLine();
                        CodeBuilder.AppendLine($"CreateMap<{entityFullName}, {modelFullName}>();").AppendLine();
                        break;
                    case ModelType.Update:
                        updateFullName = modelFullName;
                        CodeBuilder.AppendLine($"CreateMap<{entityFullName}, {modelFullName}>();").AppendLine();
                        CodeBuilder.AppendLine($"CreateMap<{modelFullName}, {entityFullName}>();").AppendLine();
                        break;
                }
            }

            // include support for coping read model to update model
            if (readFullName.HasValue() && updateFullName.HasValue())
                CodeBuilder.AppendLine($"CreateMap<{readFullName}, {updateFullName}>();").AppendLine();

        }

        CodeBuilder.AppendLine("}");
        CodeBuilder.AppendLine();
    }

}
