using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Serialization;

namespace EntityFrameworkCore.Generator;

public static class OptionMapper
{
    public static GeneratorOptions Map(GeneratorModel generator)
    {
        var options = new GeneratorOptions();
        options.Variables.ShouldEvaluate = false;

        MapProject(options.Project, generator.Project);
        MapDatabase(options.Database, generator.Database);
        MapData(options.Data, generator.Data);
        MapModel(options.Model, generator.Model);
        MapScript(options.Script, generator.Script);

        options.Variables.ShouldEvaluate = true;

        return options;
    }

    private static void MapScript(ScriptOptions option, ScriptModel? script)
    {
        if (script == null)
            return;

        MapList(option.Context, script.Context, (template) =>
        {
            var prefix = OptionsBase.AppendPrefix(option.Prefix, $"Context{option.Context.Count:0000}");
            return MapTemplate(option.Variables, template, prefix);
        });

        MapList(option.Entity, script.Entity, (template) =>
        {
            var prefix = OptionsBase.AppendPrefix(option.Prefix, $"Entity{option.Entity.Count:0000}");
            return MapTemplate(option.Variables, template, prefix);
        });

        MapList(option.Model, script.Model, (template) =>
        {
            var prefix = OptionsBase.AppendPrefix(option.Prefix, $"Model{option.Entity.Count:0000}");
            return MapTemplate(option.Variables, template, prefix);
        });
    }

    private static void MapModel(ModelOptions option, ViewModel model)
    {
        MapShared(option.Shared, model.Shared);
        MapRead(option.Read, model.Read);
        MapCreate(option.Create, model.Create);
        MapUpdate(option.Update, model.Update);
        MapMapper(option.Mapper, model.Mapper);
        MapValidator(option.Validator, model.Validator);
    }

    private static void MapClassBase(ClassOptionsBase option, ClassBase classBase)
    {
        option.Namespace = classBase.Namespace;
        option.Directory = classBase.Directory;
        option.Document = classBase.Document;
        option.Name = classBase.Name;
        option.BaseClass = classBase.BaseClass;
        option.Attributes = classBase.Attributes;
    }

    private static void MapModelBase(ModelOptionsBase option, ModelBase modelBase)
    {
        MapClassBase(option, modelBase);

        option.Generate = modelBase.Generate;

        MapSelection(option.Include, modelBase.Include);
        MapSelection(option.Exclude, modelBase.Exclude);

    }

    private static void MapValidator(ValidatorClassOptions option, ValidatorClass validator)
    {
        MapClassBase(option, validator);

        option.Generate = validator.Generate;
    }

    private static void MapMapper(MapperClassOptions option, MapperClass mapper)
    {
        MapClassBase(option, mapper);

        option.Generate = mapper.Generate;
    }

    private static void MapUpdate(UpdateModelOptions option, UpdateModel update)
    {
        MapModelBase(option, update);
    }

    private static void MapCreate(CreateModelOptions option, CreateModel create)
    {
        MapModelBase(option, create);
    }

    private static void MapRead(ReadModelOptions option, ReadModel read)
    {
        MapModelBase(option, read);
    }

    private static void MapShared(SharedModelOptions option, SharedModel shared)
    {
        option.Namespace = shared.Namespace;
        option.Directory = shared.Directory;

        MapSelection(option.Include, shared.Include);
        MapSelection(option.Exclude, shared.Exclude);
    }

    private static void MapData(DataOptions option, DataModel data)
    {
        MapContext(option.Context, data.Context);
        MapEntity(option.Entity, data.Entity);
        MapMapping(option.Mapping, data.Mapping);
        MapQuery(option.Query, data.Query);
    }

    private static void MapQuery(QueryExtensionOptions option, QueryExtension query)
    {
        MapClassBase(option, query);

        option.Generate = query.Generate;
        option.IndexPrefix = query.IndexPrefix;
        option.UniquePrefix = query.UniquePrefix;
    }

    private static void MapMapping(MappingClassOptions option, MappingClass mapping)
    {
        MapClassBase(option, mapping);

        option.Generate = mapping.Generate;
        option.Temporal = mapping.Temporal;
        option.RowVersion = mapping.RowVersion;
    }

    private static void MapEntity(EntityClassOptions option, EntityClass entity)
    {
        MapClassBase(option, entity);

        option.EntityNaming = entity.EntityNaming;
        option.RelationshipNaming = entity.RelationshipNaming;
        option.PrefixWithSchemaName = entity.PrefixWithSchemaName;
        option.MappingAttributes = entity.MappingAttributes;

        MapSelection(option.Renaming, entity.Renaming);
    }

    private static void MapSelection(SelectionOptions option, SelectionModel? selection)
    {
        if (selection == null)
            return;

        MapList(option.Entities, selection.Entities, (match) =>
        {
            var prefix = OptionsBase.AppendPrefix(option.Prefix, $"Entity{option.Entities.Count:0000}");
            return MapMatch(option.Variables, match, prefix);
        });

        MapList(option.Properties, selection.Properties, (match) =>
        {
            var prefix = OptionsBase.AppendPrefix(option.Prefix, $"Property{option.Properties.Count:0000}");
            return MapMatch(option.Variables, match, prefix);
        });
    }

    private static void MapContext(ContextClassOptions option, ContextClass context)
    {
        MapClassBase(option, context);

        option.PropertyNaming = context.PropertyNaming;
    }

    private static void MapDatabase(DatabaseOptions option, DatabaseModel database)
    {
        option.Provider = database.Provider;
        option.ConnectionString = database.ConnectionString;
        option.ConnectionName = database.ConnectionName;
        option.UserSecretsId = database.UserSecretsId;
        option.TableNaming = database.TableNaming;

        MapList(option.Tables, database.Tables);
        MapList(option.Schemas, database.Schemas);
        MapList(option.Exclude, database.Exclude, (match) =>
        {
            var prefix = OptionsBase.AppendPrefix(option.Prefix, $"Exclude{option.Exclude.Count:0000}");
            return MapMatch(option.Variables, match, prefix);
        });
    }

    private static void MapProject(ProjectOptions option, ProjectModel project)
    {
        option.Namespace = project.Namespace;
        option.Directory = project.Directory;
        option.Nullable = project.Nullable;
        option.FileScopedNamespace = project.FileScopedNamespace;
    }


    private static void MapList<T>(IList<T> targetList, IList<T>? sourceList)
    {
        if (sourceList == null || sourceList.Count == 0)
            return;

        foreach (var source in sourceList)
            targetList.Add(source);
    }

    private static void MapList<TTarget, TSource>(IList<TTarget> targetList, IList<TSource>? sourceList, Func<TSource, TTarget> factory)
    {
        if (sourceList == null || sourceList.Count == 0)
            return;

        foreach (var source in sourceList)
        {
            var target = factory(source);
            targetList.Add(target);
        }
    }

    private static MatchOptions MapMatch(VariableDictionary variables, MatchModel match, string? prefix)
    {
        return new MatchOptions(variables, prefix)
        {
            Exact = match.Exact,
            Expression = match.Expression
        };
    }

    private static TemplateOptions MapTemplate(VariableDictionary variables, TemplateModel template, string? prefix)
    {
        var option = new TemplateOptions(variables, prefix)
        {
            TemplatePath = template.TemplatePath,
            FileName = template.FileName,
            Namespace = template.Namespace,
            BaseClass = template.BaseClass,
            Directory = template.Directory,
            Overwrite = template.Overwrite,
            Merge = template.Merge,
        };

        if (template.Parameters == null || template.Parameters.Count == 0)
            return option;

        foreach (var paremeter in template.Parameters)
            option.Parameters[paremeter.Key] = paremeter.Value;

        return option;
    }
}
