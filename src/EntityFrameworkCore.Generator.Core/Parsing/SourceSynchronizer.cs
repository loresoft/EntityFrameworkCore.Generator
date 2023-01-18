using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Metadata.Parsing;
using EntityFrameworkCore.Generator.Options;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

namespace EntityFrameworkCore.Generator.Parsing;

public class SourceSynchronizer
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

        _logger.LogInformation("Parsing existing source for changes ...");

        // make sure to update the entities before the context
        UpdateFromMapping(generatedContext, options.Data.Mapping.Directory);
        UpdateFromContext(generatedContext, options.Data.Context.Directory);
        return true;
    }


    private void UpdateFromContext(EntityContext generatedContext, string contextDirectory)
    {
        if (generatedContext == null
            || contextDirectory == null
            || !Directory.Exists(contextDirectory))
            return;

        var parser = new ContextParser(_loggerFactory);

        // search all cs files looking for DbContext.  need this in case of context class rename
        ParsedContext parsedContext = null;
        using (var files = Directory.EnumerateFiles(contextDirectory, "*.cs").GetEnumerator())
            while (files.MoveNext() && parsedContext == null)
                parsedContext = parser.ParseFile(files.Current);

        if (parsedContext == null)
            return;

        if (generatedContext.ContextClass != parsedContext.ContextClass)
        {
            _logger.LogInformation(
                "Rename Context Class'{0}' to '{1}'.",
                generatedContext.ContextClass,
                parsedContext.ContextClass);

            generatedContext.ContextClass = parsedContext.ContextClass;
        }

        foreach (var parsedProperty in parsedContext.Properties)
        {
            var entity = generatedContext.Entities.ByClass(parsedProperty.EntityClass);
            if (entity == null)
                continue;

            if (entity.ContextProperty == parsedProperty.ContextProperty)
                continue;

            _logger.LogInformation(
                "Rename Context Property'{0}' to '{1}'.",
                entity.ContextProperty,
                parsedProperty.ContextProperty);

            entity.ContextProperty = parsedProperty.ContextProperty;
        }
    }

    private void UpdateFromMapping(EntityContext generatedContext, string mappingDirectory)
    {
        if (generatedContext == null
            || mappingDirectory == null
            || !Directory.Exists(mappingDirectory))
            return;

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
            // find entity by table name to support renaming entity
            var entity = generatedContext.Entities
                .ByTable(parsedEntity.TableName, parsedEntity.TableSchema);

            if (entity == null)
                continue;

            // sync names
            if (entity.MappingClass != parsedEntity.MappingClass)
            {
                _logger.LogInformation(
                    "  Rename Mapping Class'{0}' to '{1}'.",
                    entity.MappingClass,
                    parsedEntity.MappingClass);

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
        }
    }


    private void RenameEntity(EntityContext generatedContext, string originalName, string newName)
    {
        if (originalName == newName)
            return;

        _logger.LogInformation("  Rename Entity '{0}' to '{1}'.", originalName, newName);
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

        _logger.LogInformation("  Rename Property '{0}' to '{1}' in Entity '{2}'.", originalName, newName, entityName);
        foreach (var entity in generatedContext.Entities)
        {
            if (entity.EntityClass != entityName)
                continue;

            var property = entity.Properties.ByProperty(originalName);
            if (property != null)
                property.PropertyName = newName;
        }

    }
}