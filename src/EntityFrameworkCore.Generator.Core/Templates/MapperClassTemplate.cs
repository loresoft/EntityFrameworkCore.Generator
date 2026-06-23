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

        if (Options.Model.Mapper.Header.HasValue())
            CodeBuilder.AppendLine(Options.Model.Mapper.Header).AppendLine();

        CodeBuilder.AppendLine("using System;");
        CodeBuilder.AppendLine();
        CodeBuilder.AppendLine("using AutoMapper;");
        CodeBuilder.AppendLine();

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
        var entityFullName = $"{_entity.EntityNamespace}.{entityClass}";
        var mapperClass = _entity.MapperClass.ToSafeName();

        if (Options.Model.Mapper.Document)
        {
            GenerateClassDocumentation(entityFullName);
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
        var mapperFullName = $"{_entity.MapperNamespace}.{mapperClass}";

        var entityClass = _entity.EntityClass.ToSafeName();
        var entityFullName = $"{_entity.EntityNamespace}.{entityClass}";

        if (Options.Model.Mapper.Document)
        {
            GenerateConstructorDocumentation(mapperFullName, entityFullName);
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

    private void GenerateClassDocumentation(string entityClass)
    {
        var sourceName = ToXmlText(GetQualifiedTableName());
        var sourceType = _entity.IsView ? "view" : "table";
        var modelDescription = GetModelDescription();

        CodeBuilder.AppendLine("/// <summary>");

        if (sourceName.HasValue())
            CodeBuilder.AppendLine($"/// Configures AutoMapper mappings for the <see cref=\"{entityClass}\" /> entity mapped to the <c>{sourceName}</c> {sourceType}{modelDescription}.");
        else
            CodeBuilder.AppendLine($"/// Configures AutoMapper mappings for the <see cref=\"{entityClass}\" /> entity{modelDescription}.");

        CodeBuilder.AppendLine("/// </summary>");
    }

    private void GenerateConstructorDocumentation(string mapperClass, string entityClass)
    {
        CodeBuilder.AppendLine("/// <summary>");
        CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{mapperClass}\"/> class and creates mappings for <see cref=\"{entityClass}\" />.");
        CodeBuilder.AppendLine("/// </summary>");
    }

    private string GetModelDescription()
    {
        var modelTypes = _entity.Models
            .Select(model => model.ModelType switch
            {
                ModelType.Create => "create",
                ModelType.Update => "update",
                _ => "read"
            })
            .Distinct(StringComparer.Ordinal)
            .OrderBy(value => value, StringComparer.Ordinal)
            .ToList();

        return modelTypes.Count == 0
            ? string.Empty
            : $" and its generated {FormatList(modelTypes)} models";
    }

    private static string FormatList(IReadOnlyList<string> values)
    {
        return values.Count switch
        {
            0 => string.Empty,
            1 => values[0],
            2 => $"{values[0]} and {values[1]}",
            _ => $"{string.Join(", ", values.Take(values.Count - 1))}, and {values[^1]}"
        };
    }

    private string? GetQualifiedTableName()
    {
        if (_entity.TableName.IsNullOrEmpty())
            return _entity.TableName;

        return _entity.TableSchema.HasValue()
            ? $"{_entity.TableSchema}.{_entity.TableName}"
            : _entity.TableName;
    }

}
