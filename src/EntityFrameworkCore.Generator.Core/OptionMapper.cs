using System;
using System.Collections.Generic;

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

    private static void MapScript(ScriptOptions option, ScriptModel script)
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

    private static void MapValidator(ValidatorClassOptions option, ValidatorClass validator)
    {
        option.Namespace = validator.Namespace;
        option.Directory = validator.Directory;
        option.Document = validator.Document;

        option.Generate = validator.Generate;
        option.Name = validator.Name;
        option.BaseClass = validator.BaseClass;
    }

    private static void MapMapper(MapperClassOptions option, MapperClass mapper)
    {
        option.Namespace = mapper.Namespace;
        option.Directory = mapper.Directory;
        option.Document = mapper.Document;

        option.Generate = mapper.Generate;
        option.Name = mapper.Name;
        option.BaseClass = mapper.BaseClass;
    }

    private static void MapUpdate(UpdateModelOptions option, UpdateModel update)
    {
        option.Namespace = update.Namespace;
        option.Directory = update.Directory;
        option.Document = update.Document;

        option.Generate = update.Generate;
        option.Name = update.Name;
        option.BaseClass = update.BaseClass;

        MapSelection(option.Include, update.Include);
        MapSelection(option.Exclude, update.Exclude);
    }

    private static void MapCreate(CreateModelOptions option, CreateModel create)
    {
        option.Namespace = create.Namespace;
        option.Directory = create.Directory;
        option.Document = create.Document;

        option.Generate = create.Generate;
        option.Name = create.Name;
        option.BaseClass = create.BaseClass;

        MapSelection(option.Include, create.Include);
        MapSelection(option.Exclude, create.Exclude);
    }

    private static void MapRead(ReadModelOptions option, ReadModel read)
    {
        option.Namespace = read.Namespace;
        option.Directory = read.Directory;
        option.Document = read.Document;

        option.Generate = read.Generate;
        option.Name = read.Name;
        option.BaseClass = read.BaseClass;

        MapSelection(option.Include, read.Include);
        MapSelection(option.Exclude, read.Exclude);
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
        option.Namespace = query.Namespace;
        option.Directory = query.Directory;
        option.Document = query.Document;

        option.Generate = query.Generate;
        option.IndexPrefix = query.IndexPrefix;
        option.UniquePrefix = query.UniquePrefix;
    }

    private static void MapMapping(MappingClassOptions option, MappingClass mapping)
    {
        option.Namespace = mapping.Namespace;
        option.Directory = mapping.Directory;
        option.Document = mapping.Document;
    }

    private static void MapEntity(EntityClassOptions option, EntityClass entity)
    {
        option.Namespace = entity.Namespace;
        option.Directory = entity.Directory;
        option.Document = entity.Document;

        option.Name = entity.Name;
        option.BaseClass = entity.BaseClass;
        option.EntityNaming = entity.EntityNaming;
        option.RelationshipNaming = entity.RelationshipNaming;
        option.PrefixWithSchemaName = entity.PrefixWithSchemaName;

        MapSelection(option.Renaming, entity.Renaming);
    }

    private static void MapSelection(SelectionOptions option, SelectionModel selection)
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
        option.Namespace = context.Namespace;
        option.Directory = context.Directory;
        option.Document = context.Document;

        option.Name = context.Name;
        option.BaseClass = context.BaseClass;
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


    private static void MapList<T>(IList<T> targetList, IList<T> sourceList)
    {
        if (!(sourceList?.Count > 0))
            return;

        foreach (var source in sourceList)
            targetList.Add(source);
    }

    private static void MapList<TTarget, TSource>(IList<TTarget> targetList, IList<TSource> sourceList, Func<TSource, TTarget> factory)
    {
        if (sourceList == null || sourceList.Count == 0)
            return;

        foreach (var source in sourceList)
        {
            var target = factory(source);
            targetList.Add(target);
        }
    }

    private static MatchOptions MapMatch(VariableDictionary variables, MatchModel match, string prefix)
    {
        return new MatchOptions(variables, prefix)
        {
            Exact = match.Exact,
            Expression = match.Expression
        };
    }

    private static TemplateOptions MapTemplate(VariableDictionary variables, TemplateModel template, string prefix)
    {
        var option = new TemplateOptions(variables, prefix)
        {
            TemplatePath = template.TemplatePath,
            FileName = template.FileName,
            Namespace = template.Namespace,
            BaseClass = template.BaseClass,
            Directory = template.Directory,
            Overwrite = template.Overwrite,
        };

        if (template.Parameters == null || template.Parameters.Count == 0)
            return option;

        foreach (var paremeter in template.Parameters)
            option.Parameters[paremeter.Key] = paremeter.Value;

        return option;
    }

}
