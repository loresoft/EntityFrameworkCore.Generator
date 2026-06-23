using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Metadata.Parsing;
using EntityFrameworkCore.Generator.Options;

using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Parsing;

public partial class SourceSynchronizer
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;

    public SourceSynchronizer(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<SourceSynchronizer>();
    }

    public bool UpdateFromSource(EntityContext generatedContext, GeneratorOptions options)
    {
        if (generatedContext == null)
            return false;

        LogParsingExistingSource(_logger);

        // make sure to update the entities before the context
        UpdateFromMapping(generatedContext, options.Data.Mapping.Directory);
        UpdateFromContext(generatedContext, options.Data.Context.Directory);
        UpdateFromModels(generatedContext, options);

        return true;
    }

    private void UpdateFromModels(EntityContext generatedContext, GeneratorOptions options)
    {
        if (generatedContext == null || options == null)
            return;

        var parser = new ModelParser(_loggerFactory);

        foreach (var entity in generatedContext.Entities)
        {
            options.Variables.Set(entity);

            foreach (var model in entity.Models)
            {
                options.Variables.Set(model);

                UpdateFromModel(parser, model, options);

                options.Variables.Remove(model);
            }

            options.Variables.Remove(entity);
        }
    }

    private void UpdateFromModel(ModelParser parser, Model model, GeneratorOptions options)
    {
        var modelDirectory = GetModelDirectory(model, options) ?? "Data\\Models";
        modelDirectory = NormalizeDirectory(modelDirectory);

        if (!Directory.Exists(modelDirectory))
            return;

        var modelFile = Path.Combine(modelDirectory, model.ModelClass + ".cs");
        var parsedModel = parser.ParseFile(modelFile);
        if (parsedModel == null || parsedModel.ModelClass != model.ModelClass)
            return;

        foreach (var parsedProperty in parsedModel.Properties)
        {
            var property = model.Properties.ByProperty(parsedProperty.PropertyName);
            if (property == null)
                continue;

            LogPreserveAttributesForModelProperty(_logger, parsedProperty.PropertyName, model.ModelClass);

            model.PropertyAttributes[parsedProperty.PropertyName] = [.. parsedProperty.Attributes];
        }
    }

    private static string? GetModelDirectory(Model model, GeneratorOptions options)
    {
        if (model.ModelType == ModelType.Create)
        {
            return options.Model.Create.Directory.HasValue()
                ? options.Model.Create.Directory
                : options.Model.Shared.Directory;
        }

        if (model.ModelType == ModelType.Update)
        {
            return options.Model.Update.Directory.HasValue()
                ? options.Model.Update.Directory
                : options.Model.Shared.Directory;
        }

        return options.Model.Read.Directory.HasValue()
            ? options.Model.Read.Directory
            : options.Model.Shared.Directory;
    }


    private void UpdateFromContext(EntityContext generatedContext, string? contextDirectory)
    {
        contextDirectory = NormalizeDirectory(contextDirectory);

        if (generatedContext == null
            || contextDirectory == null
            || !Directory.Exists(contextDirectory))
        {
            return;
        }

        var parser = new ContextParser(_loggerFactory);

        // search all cs files looking for DbContext.  need this in case of context class rename
        ParsedContext? parsedContext = null;

        using var files = Directory.EnumerateFiles(contextDirectory, "*.cs").GetEnumerator();
        while (files.MoveNext() && parsedContext == null)
            parsedContext = parser.ParseFile(files.Current);

        if (parsedContext == null)
            return;

        if (generatedContext.ContextClass != parsedContext.ContextClass)
        {
            LogRenameContextClass(_logger, generatedContext.ContextClass, parsedContext.ContextClass);

            generatedContext.ContextClass = parsedContext.ContextClass;
        }

        foreach (var parsedProperty in parsedContext.Properties)
        {
            var entity = generatedContext.Entities.ByClass(parsedProperty.EntityClass);
            if (entity == null)
                continue;

            if (entity.ContextProperty == parsedProperty.ContextProperty)
                continue;

            LogRenameContextProperty(_logger, entity.ContextProperty, parsedProperty.ContextProperty);

            entity.ContextProperty = parsedProperty.ContextProperty;
        }
    }

    private void UpdateFromMapping(EntityContext generatedContext, string? mappingDirectory)
    {
        mappingDirectory = NormalizeDirectory(mappingDirectory);

        if (generatedContext == null
            || mappingDirectory == null
            || !Directory.Exists(mappingDirectory))
        {
            return;
        }

        var parser = new MappingParser(_loggerFactory);

        // parse all mapping files
        var mappingFiles = Directory.EnumerateFiles(mappingDirectory, "*.cs");
        var parsedEntities = mappingFiles
            .Select(parser.ParseFile)
            .Where(parsedEntity => parsedEntity != null)
            .ToList();

        // update all entity and property names first because relationships are linked by property names
        foreach (var parsedEntity in parsedEntities)
        {
            if (parsedEntity == null)
                continue;

            // find entity by table name to support renaming entity
            var entity = generatedContext.Entities
                .ByTable(parsedEntity.TableName, parsedEntity.TableSchema);

            if (entity == null)
                continue;

            // sync names
            if (entity.MappingClass != parsedEntity.MappingClass)
            {
                LogRenameMappingClass(_logger, entity.MappingClass, parsedEntity.MappingClass);

                entity.MappingClass = parsedEntity.MappingClass;
            }

            RenameEntity(generatedContext, entity.EntityClass, parsedEntity.EntityClass);

            // sync properties
            foreach (var parsedProperty in parsedEntity.Properties)
            {
                // find property by column name to support property name rename
                var property = entity.Properties.ByColumn(parsedProperty.ColumnName);
                if (property == null)
                    continue;

                RenameProperty(
                    generatedContext,
                    entity.EntityClass,
                    property.PropertyName,
                    parsedProperty.PropertyName);
            }

            // sync relationships
            foreach (var parsedRelationship in parsedEntity.Relationships)
            {
                var relationship = entity.Relationships.FirstOrDefault(r => r.RelationshipName == parsedRelationship.RelationshipName && r.IsForeignKey);
                if (relationship == null)
                    continue;

                RenameRelationship(parsedRelationship, relationship);
            }
        }
    }

    private static string? NormalizeDirectory(string? directory)
    {
        if (directory.IsNullOrWhiteSpace() || Path.DirectorySeparatorChar == '\\')
            return directory;

        return directory.Replace('\\', Path.DirectorySeparatorChar);
    }



    private void RenameEntity(EntityContext generatedContext, string originalName, string newName)
    {
        if (originalName == newName)
            return;

        LogRenameEntity(_logger, originalName, newName);
        foreach (var entity in generatedContext.Entities)
        {
            if (entity.EntityClass == originalName)
                entity.EntityClass = newName;
        }
    }

    private void RenameProperty(EntityContext generatedContext, string entityName, string originalName, string newName)
    {
        if (originalName == newName)
            return;

        LogRenameProperty(_logger, originalName, newName, entityName);
        foreach (var entity in generatedContext.Entities)
        {
            if (entity.EntityClass != entityName)
                continue;

            var property = entity.Properties.ByProperty(originalName);
            if (property != null)
                property.PropertyName = newName;
        }

    }

    private void RenameRelationship(ParsedRelationship parsedRelationship, Relationship relationship)
    {
        if (parsedRelationship.PropertyName.HasValue() && relationship.PropertyName != parsedRelationship.PropertyName)
        {
            LogRenameProperty(_logger, relationship.PropertyName, parsedRelationship.PropertyName, relationship.Entity.EntityClass);
            relationship.PropertyName = parsedRelationship.PropertyName;
        }

        if (parsedRelationship.PrimaryPropertyName.HasValue() && relationship.PrimaryPropertyName != parsedRelationship.PrimaryPropertyName)
        {
            LogRenameProperty(_logger, relationship.PrimaryPropertyName, parsedRelationship.PrimaryPropertyName, relationship.PrimaryEntity.EntityClass);
            relationship.PrimaryPropertyName = parsedRelationship.PrimaryPropertyName;
        }

        var primaryEntity = relationship.PrimaryEntity;
        var primaryRelationship = primaryEntity.Relationships.FirstOrDefault(r => r.RelationshipName == parsedRelationship.RelationshipName && !r.IsForeignKey);

        if (primaryRelationship == null)
            return;

        if (parsedRelationship.PrimaryPropertyName.HasValue() && primaryRelationship.PropertyName != parsedRelationship.PrimaryPropertyName)
        {
            LogRenameProperty(_logger, primaryRelationship.PropertyName, parsedRelationship.PrimaryPropertyName, primaryRelationship.Entity.EntityClass);
            primaryRelationship.PropertyName = parsedRelationship.PrimaryPropertyName;
        }

        if (parsedRelationship.PropertyName.HasValue() && primaryRelationship.PrimaryPropertyName != parsedRelationship.PropertyName)
        {
            LogRenameProperty(_logger, primaryRelationship.PrimaryPropertyName, parsedRelationship.PropertyName, primaryRelationship.PrimaryEntity.EntityClass);
            primaryRelationship.PrimaryPropertyName = parsedRelationship.PropertyName;
        }
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Parsing existing source for changes ...")]
    private static partial void LogParsingExistingSource(ILogger logger);

    [LoggerMessage(EventId = 2, Level = LogLevel.Debug, Message = "  Preserve attributes for Model Property '{propertyName}' in Model '{modelClass}'.")]
    private static partial void LogPreserveAttributesForModelProperty(ILogger logger, string propertyName, string modelClass);

    [LoggerMessage(EventId = 3, Level = LogLevel.Information, Message = "Rename Context Class'{originalContextClass}' to '{parsedContextClass}'.")]
    private static partial void LogRenameContextClass(ILogger logger, string originalContextClass, string parsedContextClass);

    [LoggerMessage(EventId = 4, Level = LogLevel.Information, Message = "Rename Context Property'{entityProperty}' to '{parsedProperty}'.")]
    private static partial void LogRenameContextProperty(ILogger logger, string entityProperty, string parsedProperty);

    [LoggerMessage(EventId = 5, Level = LogLevel.Debug, Message = "  Rename Mapping Class'{entityClass}' to '{parsedClass}'.")]
    private static partial void LogRenameMappingClass(ILogger logger, string entityClass, string parsedClass);

    [LoggerMessage(EventId = 6, Level = LogLevel.Debug, Message = "  Rename Entity '{originalName}' to '{newName}'.")]
    private static partial void LogRenameEntity(ILogger logger, string originalName, string newName);

    [LoggerMessage(EventId = 7, Level = LogLevel.Debug, Message = "  Rename Property '{originalName}' to '{newName}' in Entity '{entityName}'.")]
    private static partial void LogRenameProperty(ILogger logger, string? originalName, string? newName, string entityName);
}
