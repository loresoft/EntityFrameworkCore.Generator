using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Providers;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
{
    public class ModelGenerator
    {
        private readonly UniqueNamer _namer;
        private readonly ILogger _logger;
        private GeneratorOptions _options;
        private IProviderTypeMapping _typeMapper;

        public ModelGenerator(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<ModelGenerator>();
            _namer = new UniqueNamer();
        }

        public EntityContext Generate(GeneratorOptions options, DatabaseModel databaseModel)
        {
            if (databaseModel == null)
                throw new ArgumentNullException(nameof(databaseModel));

            _options = options ?? throw new ArgumentNullException(nameof(options));
            _typeMapper = GetTypeMapper();

            var entityContext = new EntityContext();
            entityContext.DatabaseName = databaseModel.DatabaseName;

            if (_options.Database.Name.IsNullOrWhiteSpace())
                _options.Database.Name = databaseModel.DatabaseName;

            string projectNamespace = NameFormatter.Format(_options.Project.Namespace, _options);
            _options.Project.Namespace = projectNamespace;

            string contextClass = NameFormatter.Format(_options.Data.Context.Name, _options);
            contextClass = _namer.UniqueClassName(contextClass);

            string contextNamespace = NameFormatter.Format(_options.Data.Context.Namespace, _options);
            string contextBaseClass = NameFormatter.Format(_options.Data.Context.BaseClass, _options);

            entityContext.ContextClass = contextClass;
            entityContext.ContextNamespace = contextNamespace;
            entityContext.ContextBaseClass = contextBaseClass;

            var tables = databaseModel.Tables;

            foreach (var t in tables)
            {
                _logger.LogInformation($"Getting Table Schema: {t}");
                var entity = GetEntity(entityContext, t);
                GetModels(entity);
            }

            return entityContext;
        }

        private void GetModels(Entity entity)
        {
            if (entity == null || entity.Models.IsProcessed)
                return;

            if (_options.Model.Read.Generate)
                CreateReadModel(entity);
            if (_options.Model.Create.Generate)
                CreateInsertModel(entity);
            if (_options.Model.Update.Generate)
                CreateUpdateModel(entity);

            entity.Models.IsProcessed = true;
        }

        private void CreateUpdateModel(Entity entity)
        {
            var model = new Model { Entity = entity };

            entity.Models.Add(model);
        }

        private void CreateInsertModel(Entity entity)
        {
            var model = new Model { Entity = entity };

            string modellass = NameFormatter.Format(_options.Model.Create.Name, _options);


            entity.Models.Add(model);
        }

        private void CreateReadModel(Entity entity)
        {
            var model = new Model { Entity = entity };

            entity.Models.Add(model);
        }


        private Entity GetEntity(EntityContext entityContext, DatabaseTable tableSchema, bool processRelationships = true, bool processMethods = true)
        {
            Entity entity = entityContext.Entities.ByTable(tableSchema.Name, tableSchema.Schema)
                ?? CreateEntity(entityContext, tableSchema);

            if (!entity.Properties.IsProcessed)
                CreateProperties(entity, tableSchema.Columns);

            if (processRelationships && !entity.Relationships.IsProcessed)
                CreateRelationships(entityContext, entity, tableSchema);

            if (processMethods && !entity.Methods.IsProcessed)
                CreateMethods(entity, tableSchema);

            entity.IsProcessed = true;
            return entity;
        }

        private Entity CreateEntity(EntityContext entityContext, DatabaseTable tableSchema)
        {
            var entity = new Entity
            {
                Context = entityContext,
                TableName = tableSchema.Name,
                TableSchema = tableSchema.Schema
            };

            string entityClass = ToClassName(tableSchema.Name);
            entityClass = _namer.UniqueClassName(entityClass);

            string entityNamespace = NameFormatter.Format(_options.Data.Entity.Namespace, _options);
            string entiyBaseClass = NameFormatter.Format(_options.Data.Entity.BaseClass, _options);


            string mappingName = entityClass + "Map";
            mappingName = _namer.UniqueClassName(mappingName);

            string mappingNamespace = NameFormatter.Format(_options.Data.Mapping.Namespace, _options);

            string contextName = ContextName(entityClass);
            contextName = ToPropertyName(entityContext.ContextClass, contextName);
            contextName = _namer.UniqueContextName(contextName);

            entity.EntityClass = entityClass;
            entity.EntityNamespace = entityNamespace;
            entity.EntityBaseClass = entiyBaseClass;

            entity.MappingClass = mappingName;
            entity.MappingNamespace = mappingNamespace;

            entity.ContextProperty = contextName;

            entityContext.Entities.Add(entity);

            return entity;
        }


        private void CreateProperties(Entity entity, IEnumerable<DatabaseColumn> columns)
        {
            foreach (var column in columns)
            {
                var table = column.Table;
                var property = entity.Properties.ByColumn(column.Name);

                if (property == null)
                {
                    property = new Property<Entity>
                    {
                        Entity = entity,
                        ColumnName = column.Name
                    };
                    entity.Properties.Add(property);
                }

                string propertyName = ToPropertyName(entity.EntityClass, column.Name);
                propertyName = _namer.UniqueName(entity.EntityClass, propertyName);

                property.PropertyName = propertyName;

                property.IsNullable = column.IsNullable;

                property.IsRowVersion = column.ValueGenerated == ValueGenerated.OnAddOrUpdate
                    && (bool?)column[ScaffoldingAnnotationNames.ConcurrencyToken] == true;

                property.IsPrimaryKey = table.PrimaryKey?.Columns.Contains(column) == true;
                property.IsForeignKey = table.ForeignKeys.Any(c => c.Columns.Contains(column));

                property.IsUnique = table.UniqueConstraints.Any(c => c.Columns.Contains(column))
                    || table.Indexes.Where(i => i.IsUnique).Any(c => c.Columns.Contains(column));

                property.Default = column.DefaultValueSql;
                property.ValueGenerated = column.ValueGenerated;

                var mapping = _typeMapper.ParseType(column.StoreType);
                property.StoreType = mapping.StoreType;
                property.NativeType = mapping.NativeType;
                property.DataType = mapping.DataType;
                property.SystemType = mapping.SystemType;
                property.IsMaxLength = mapping.IsMaxLength;
                property.Size = mapping.Size;
                property.Precision = mapping.Precision;
                property.Scale = mapping.Scale;

                property.IsProcessed = true;
            }

            entity.Properties.IsProcessed = true;
        }


        private void CreateRelationships(EntityContext entityContext, Entity entity, DatabaseTable tableSchema)
        {
            foreach (var foreignKey in tableSchema.ForeignKeys)
            {
                CreateRelationship(entityContext, entity, foreignKey);
            }

            entity.Relationships.IsProcessed = true;
        }

        private void CreateRelationship(EntityContext entityContext, Entity foreignEntity, DatabaseForeignKey tableKeySchema)
        {
            Entity primaryEntity = GetEntity(entityContext, tableKeySchema.PrincipalTable, false, false);

            string primaryName = primaryEntity.EntityClass;
            string foreignName = foreignEntity.EntityClass;

            string relationshipName = tableKeySchema.Name;
            relationshipName = _namer.UniqueRelationshipName(relationshipName);

            bool isCascadeDelete = false; //IsCascadeDelete(tableKeySchema);

            var foreignMembers = GetKeyMembers(foreignEntity, tableKeySchema.Columns, tableKeySchema.Name);
            bool foreignMembersRequired = foreignMembers.Any(c => c.IsRequired);

            var primaryMembers = GetKeyMembers(primaryEntity, tableKeySchema.PrincipalColumns, tableKeySchema.Name);
            bool primaryMembersRequired = primaryMembers.Any(c => c.IsRequired);

            // skip invalid fkeys
            if (foreignMembers.Count == 0 || primaryMembers.Count == 0)
                return;

            Relationship foreignRelationship = foreignEntity.Relationships
                .FirstOrDefault(r => r.RelationshipName == relationshipName && r.IsForeignKey);

            if (foreignRelationship == null)
            {
                foreignRelationship = new Relationship
                {
                    RelationshipName = relationshipName
                };
                foreignEntity.Relationships.Add(foreignRelationship);
            }
            foreignRelationship.IsMapped = true;
            foreignRelationship.IsForeignKey = true;
            foreignRelationship.Cardinality = foreignMembersRequired ? Cardinality.One : Cardinality.ZeroOrOne;

            foreignRelationship.PrimaryEntity = primaryEntity;
            foreignRelationship.PrimaryProperties = new PropertyCollection<Entity>(primaryMembers);

            foreignRelationship.Entity = foreignEntity;
            foreignRelationship.Properties = new PropertyCollection<Entity>(foreignMembers);

            string prefix = GetMemberPrefix(foreignRelationship, primaryName, foreignName);

            string foreignPropertyName = ToPropertyName(foreignEntity.EntityClass, prefix + primaryName);
            foreignPropertyName = _namer.UniqueName(foreignEntity.EntityClass, foreignPropertyName);
            foreignRelationship.PropertyName = foreignPropertyName;

            // add reverse
            Relationship primaryRelationship = primaryEntity.Relationships
                .FirstOrDefault(r => r.RelationshipName == relationshipName && r.IsForeignKey == false);

            if (primaryRelationship == null)
            {
                primaryRelationship = new Relationship { RelationshipName = relationshipName };
                primaryEntity.Relationships.Add(primaryRelationship);
            }

            primaryRelationship.IsMapped = false;
            primaryRelationship.IsForeignKey = false;

            primaryRelationship.PrimaryEntity = foreignEntity;
            primaryRelationship.PrimaryProperties = new PropertyCollection<Entity>(foreignMembers);

            primaryRelationship.Entity = primaryEntity;
            primaryRelationship.Properties = new PropertyCollection<Entity>(primaryMembers);

            bool isOneToOne = IsOneToOne(tableKeySchema, foreignRelationship);
            if (isOneToOne)
                primaryRelationship.Cardinality = primaryMembersRequired ? Cardinality.One : Cardinality.ZeroOrOne;
            else
                primaryRelationship.Cardinality = Cardinality.Many;

            string primaryPropertyName = prefix + foreignName;
            if (!isOneToOne)
                primaryPropertyName = RelationshipName(primaryPropertyName);

            primaryPropertyName = ToPropertyName(primaryEntity.EntityClass, primaryPropertyName);
            primaryPropertyName = _namer.UniqueName(primaryEntity.EntityClass, primaryPropertyName);

            primaryRelationship.PropertyName = primaryPropertyName;

            foreignRelationship.PrimaryPropertyName = primaryRelationship.PropertyName;
            foreignRelationship.PrimaryCardinality = primaryRelationship.Cardinality;

            primaryRelationship.PrimaryPropertyName = foreignRelationship.PropertyName;
            primaryRelationship.PrimaryCardinality = foreignRelationship.Cardinality;

            foreignRelationship.IsProcessed = true;
            primaryRelationship.IsProcessed = true;
        }


        private void CreateMethods(Entity entity, DatabaseTable tableSchema)
        {
            if (tableSchema.PrimaryKey != null)
            {
                var method = GetMethodFromColumns(entity, tableSchema.PrimaryKey.Columns);
                if (method != null)
                {
                    method.IsKey = true;
                    method.SourceName = tableSchema.PrimaryKey.Name;

                    if (entity.Methods.All(m => m.NameSuffix != method.NameSuffix))
                        entity.Methods.Add(method);
                }
            }

            GetIndexMethods(entity, tableSchema);
            GetForeignKeyMethods(entity, tableSchema);

            entity.Methods.IsProcessed = true;
        }

        private void GetForeignKeyMethods(Entity entity, DatabaseTable table)
        {
            var columns = new List<DatabaseColumn>();

            foreach (var column in table.ForeignKeys.SelectMany(c => c.Columns))
            {
                columns.Add(column);

                var method = GetMethodFromColumns(entity, columns);
                if (method != null && entity.Methods.All(m => m.NameSuffix != method.NameSuffix))
                    entity.Methods.Add(method);

                columns.Clear();
            }
        }

        private void GetIndexMethods(Entity entity, DatabaseTable table)
        {
            foreach (var index in table.Indexes)
            {
                var method = GetMethodFromColumns(entity, index.Columns);
                if (method == null)
                    continue;

                method.SourceName = index.Name;
                method.IsUnique = index.IsUnique;
                method.IsIndex = true;

                if (entity.Methods.All(m => m.NameSuffix != method.NameSuffix))
                    entity.Methods.Add(method);
            }
        }

        private Method GetMethodFromColumns(Entity entity, IEnumerable<DatabaseColumn> columns)
        {
            var method = new Method { Entity = entity };
            var methodName = new StringBuilder();

            foreach (var column in columns)
            {
                var property = entity.Properties.ByColumn(column.Name);
                if (property == null)
                    continue;

                method.Properties.Add(property);
                methodName.Append(property.PropertyName);
            }

            if (method.Properties.Count == 0)
                return null;

            method.NameSuffix = methodName.ToString();
            return method;
        }


        private List<Property<Entity>> GetKeyMembers(Entity entity, IEnumerable<DatabaseColumn> members, string relationshipName)
        {
            var keyMembers = new List<Property<Entity>>();

            foreach (var member in members)
            {
                var property = entity.Properties.ByColumn(member.Name);

                if (property == null)
                    _logger.LogWarning("Could not find column {0} for relationship {1}.", member.Name, relationshipName);
                else
                    keyMembers.Add(property);
            }

            return keyMembers;
        }

        private string GetMemberPrefix(Relationship relationship, string primaryClass, string foreignClass)
        {
            string thisKey = relationship.Properties
                .Select(p => p.PropertyName)
                .FirstOrDefault() ?? string.Empty;

            string otherKey = relationship.PrimaryProperties
                .Select(p => p.PropertyName)
                .FirstOrDefault() ?? string.Empty;

            bool isSameName = thisKey.Equals(otherKey, StringComparison.OrdinalIgnoreCase);
            isSameName = (isSameName || thisKey.Equals(primaryClass + otherKey, StringComparison.OrdinalIgnoreCase));

            string prefix = string.Empty;
            if (isSameName)
                return prefix;

            prefix = thisKey.Replace(otherKey, "");
            prefix = prefix.Replace(primaryClass, "");
            prefix = prefix.Replace(foreignClass, "");
            prefix = Regex.Replace(prefix, @"(_ID|_id|_Id|\.ID|\.id|\.Id|ID|Id)$", "");
            prefix = Regex.Replace(prefix, @"^\d", "");

            return prefix;
        }

        private bool IsOneToOne(DatabaseForeignKey tableKeySchema, Relationship foreignRelationship)
        {
            var foreignColumn = foreignRelationship.Properties
                .Select(p => p.ColumnName)
                .FirstOrDefault();

            bool isFkeyPkey = tableKeySchema.PrincipalTable.PrimaryKey != null
                              && tableKeySchema.Table.PrimaryKey != null
                              && tableKeySchema.Table.PrimaryKey.Columns.Count == 1
                              && tableKeySchema.Table.PrimaryKey.Columns.Any(c => c.Name == foreignColumn);

            if (isFkeyPkey)
                return true;

            return false;

            // if f.key is unique
            //return tableKeySchema.ForeignKeyMemberColumns.All(column => column.IsUnique);
        }


        #region Name Helpers
        private string RelationshipName(string name)
        {
            var naming = _options.Data.Entity.RelationshipNaming;
            if (naming == RelationshipNaming.Preserve)
                return name;

            if (naming == RelationshipNaming.Suffix)
                return name + "List";

            return name.Pluralize();
        }

        private string ContextName(string name)
        {
            var naming = _options.Data.Context.PropertyNaming;
            if (naming == ContextNaming.Preserve)
                return name;

            if (naming == ContextNaming.Suffix)
                return name + "DataSet";

            return name.Pluralize();
        }

        private string EntityName(string name)
        {
            var tableNaming = _options.Database.TableNaming;
            var entityNaming = _options.Data.Entity.EntityNaming;

            if (tableNaming != TableNaming.Plural && entityNaming == EntityNaming.Plural)
                name = name.Pluralize();
            else if (tableNaming != TableNaming.Singular && entityNaming == EntityNaming.Singular)
                name = name.Singularize();

            return name;
        }


        private string ToClassName(string name)
        {
            name = EntityName(name);
            string legalName = ToLegalName(name);

            return legalName;
        }

        private string ToPropertyName(string className, string name)
        {
            string propertyName = ToLegalName(name);
            if (className.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
                propertyName += "Member";

            return propertyName;
        }

        private string ToLegalName(string name)
        {
            string legalName = name;
            legalName = legalName.ToPascalCase();

            return legalName;
        }
        #endregion


        private IProviderTypeMapping GetTypeMapper()
        {
            var provider = _options.Database.Provider;

            _logger.LogTrace($"Creating database model factory for: {provider}");
            if (provider == DatabaseProviders.SqlServer)
                return new SqlServerTypeMapping();

            //if (provider == DatabaseProviders.PostgreSQL)
            //    return new Npgsql.EntityFrameworkCore.PostgreSQL.Scaffolding.Internal.NpgsqlDatabaseModelFactory(_diagnosticsLogger);

            //if (Options.Database.Provider == DatabaseProviders.Sqlite)
            //    return new Microsoft.EntityFrameworkCore.Sqlite.Scaffolding.Internal.SqliteDatabaseModelFactory(_diagnosticsLogger, null);

            throw new NotSupportedException($"The specified provider '{provider}' is not supported.");
        }

    }
}