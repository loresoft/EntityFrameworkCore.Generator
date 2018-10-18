using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using EntityFrameworkCore.Generator.Metadata.Generation;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class SourceSynchronizer
    {
        public bool UpdateFromSource(EntityContext generatedContext, string contextDirectory, string mappingDirectory)
        {
            if (generatedContext == null)
                return false;

            // make sure to update the entities before the context
            UpdateFromMapping(generatedContext, mappingDirectory);
            UpdateFromContext(generatedContext, contextDirectory);
            return true;
        }

        private void UpdateFromContext(EntityContext generatedContext, string contextDirectory)
        {
            if (generatedContext == null
              || contextDirectory == null
              || !Directory.Exists(contextDirectory))
                return;

            var parser = new ContextParser();

            // parse context
            ParsedContext parsedContext = null;
            using (var files = Directory.EnumerateFiles(contextDirectory, "*.Generated.cs").GetEnumerator())
            {
                while (files.MoveNext() && parsedContext == null)
                    parsedContext = parser.Parse(files.Current);
            }

            if (parsedContext == null)
                return;

            if (generatedContext.ContextClass != parsedContext.ContextClass)
            {
                Debug.WriteLine("Rename Context Class'{0}' to '{1}'.",
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

                Debug.WriteLine("Rename Context Property'{0}' to '{1}'.",
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

            var parser = new MappingParser();

            // parse all mapping files
            var mappingFiles = Directory.EnumerateFiles(mappingDirectory, "*.Generated.cs");
            var parsedEntities = mappingFiles
              .Select(parser.Parse)
              .Where(parsedEntity => parsedEntity != null)
              .ToList();

            var relationshipQueue = new List<Tuple<Entity, ParsedEntity>>();

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
                    Debug.WriteLine("Rename Mapping Class'{0}' to '{1}'.",
                          entity.MappingClass,
                          parsedEntity.MappingClass);

                    entity.MappingClass = parsedEntity.MappingClass;
                }

                // use rename api to make sure all instances are renamed
                generatedContext.RenameEntity(entity.EntityClass, parsedEntity.EntityClass);

                // sync properties
                foreach (var parsedProperty in parsedEntity.Properties)
                {
                    // find property by column name to support property name rename
                    var property = entity.Properties.ByColumn(parsedProperty.ColumnName);
                    if (property == null)
                        continue;

                    // use rename api to make sure all instances are renamed
                    generatedContext.RenameProperty(
                      entity.EntityClass,
                      property.PropertyName,
                      parsedProperty.PropertyName);
                }

                // save relationship for later processing
                var item = new Tuple<Entity, ParsedEntity>(entity, parsedEntity);
                relationshipQueue.Add(item);
            }

            // update relationships last
            foreach (var tuple in relationshipQueue)
                UpdateRelationships(generatedContext, tuple.Item1, tuple.Item2);
        }

        private void UpdateRelationships(EntityContext generatedContext, Entity entity, ParsedEntity parsedEntity)
        {
            // sync relationships
            //foreach (var parsedRelationship in parsedEntity.Relationships)
            //{
            //    var parsedProperties = parsedRelationship.ThisProperties;
            //    var relationship = entity.Relationships
            //        .FirstOrDefault(r => !r.ThisProperties.Except(parsedProperties).Any());

            //    if (relationship == null)
            //        continue;

            //    bool isThisSame = relationship.ThisPropertyName == parsedRelationship.ThisPropertyName;
            //    bool isOtherSame = relationship.OtherPropertyName == parsedRelationship.OtherPropertyName;

            //    if (isThisSame && isOtherSame)
            //        continue;

            //    if (!isThisSame)
            //    {
            //        Debug.WriteLine("Rename Relationship Property '{0}.{1}' to '{0}.{2}'.",
            //              relationship.ThisEntity,
            //              relationship.ThisPropertyName,
            //              parsedRelationship.ThisPropertyName);

            //        relationship.ThisPropertyName = parsedRelationship.ThisPropertyName;
            //    }
            //    if (!isOtherSame)
            //    {
            //        Debug.WriteLine("Rename Relationship Property '{0}.{1}' to '{0}.{2}'.",
            //              relationship.OtherEntity,
            //              relationship.OtherPropertyName,
            //              parsedRelationship.OtherPropertyName);

            //        relationship.OtherPropertyName = parsedRelationship.OtherPropertyName;
            //    }

            //    // sync other relationship
            //    var otherEntity = generatedContext.Entities.ByClass(relationship.OtherEntity);
            //    if (otherEntity == null)
            //        continue;

            //    var otherRelationship = otherEntity.Relationships.ByName(relationship.RelationshipName);
            //    if (otherRelationship == null)
            //        continue;

            //    otherRelationship.ThisPropertyName = relationship.OtherPropertyName;
            //    otherRelationship.OtherPropertyName = relationship.ThisPropertyName;
            //}
        }
    }
}
