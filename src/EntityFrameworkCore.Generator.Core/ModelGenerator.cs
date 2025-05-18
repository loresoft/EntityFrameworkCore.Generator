using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

using Humanizer;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

using Model = EntityFrameworkCore.Generator.Metadata.Generation.Model;
using Property = EntityFrameworkCore.Generator.Metadata.Generation.Property;
using PropertyCollection = EntityFrameworkCore.Generator.Metadata.Generation.PropertyCollection;

namespace EntityFrameworkCore.Generator;

public partial class ModelGenerator
{
    private readonly UniqueNamer _namer;
    private readonly ILogger _logger;
    private GeneratorOptions _options = null!;
    private IRelationalTypeMappingSource _typeMapper = null!;

    public ModelGenerator(ILoggerFactory logger)
    {
        _logger = logger.CreateLogger<ModelGenerator>();
        _namer = new UniqueNamer();
    }

    public EntityContext Generate(GeneratorOptions options, DatabaseModel databaseModel, IRelationalTypeMappingSource typeMappingSource)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(databaseModel);
        ArgumentNullException.ThrowIfNull(typeMappingSource);

        _logger.LogInformation("Building code generation model from database: {databaseName}", databaseModel.DatabaseName);

        _options = options;
        _typeMapper = typeMappingSource;

        var entityContext = new EntityContext();
        entityContext.DatabaseName = databaseModel.DatabaseName;

        // update database variables
        _options.Database.Name = ToLegalName(databaseModel.DatabaseName);


        string contextClass = _options.Data.Context.Name ?? "DataContext";
        contextClass = _namer.UniqueClassName(contextClass);

        string contextNamespace = _options.Data.Context.Namespace ?? "Data";
        string contextBaseClass = _options.Data.Context.BaseClass ?? "DbContext";

        entityContext.ContextClass = contextClass;
        entityContext.ContextNamespace = contextNamespace;
        entityContext.ContextBaseClass = contextBaseClass;

        var tables = databaseModel.Tables;

        foreach (var table in tables)
        {
            if (IsIgnored(table, _options.Database.Exclude.Tables))
            {
                _logger.LogDebug("  Skipping Table : {schema}.{name}", table.Schema, table.Name);
                continue;
            }

            _logger.LogDebug("  Processing Table : {schema}.{name}", table.Schema, table.Name);

            _options.Variables.Set(VariableConstants.TableSchema, ToLegalName(table.Schema));
            _options.Variables.Set(VariableConstants.TableName, ToLegalName(table.Name));

            var entity = GetEntity(entityContext, table);
            GetModels(entity);
        }

        _options.Variables.Remove(VariableConstants.TableName);
        _options.Variables.Remove(VariableConstants.TableSchema);

        return entityContext;
    }


    private Entity GetEntity(EntityContext entityContext, DatabaseTable tableSchema, bool processRelationships = true, bool processMethods = true)
    {
        var entity = entityContext.Entities.ByTable(tableSchema.Name, tableSchema.Schema)
            ?? CreateEntity(entityContext, tableSchema);

        if (!entity.Properties.IsProcessed)
            CreateProperties(entity, tableSchema);

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

        var entityClass = _options.Data.Entity.Name;
        if (entityClass.IsNullOrEmpty())
            entityClass = ToClassName(tableSchema.Name, tableSchema.Schema);

        entityClass = _namer.UniqueClassName(entityClass);

        var entityNamespace = _options.Data.Entity.Namespace ?? "Data.Entities";
        var entiyBaseClass = _options.Data.Entity.BaseClass;


        var mappingName = entityClass + "Map";
        mappingName = _namer.UniqueClassName(mappingName);

        var mappingNamespace = _options.Data.Mapping.Namespace ?? "Data.Mapping";

        var contextName = ContextName(entityClass);
        contextName = ToPropertyName(entityContext.ContextClass, contextName);
        contextName = _namer.UniqueContextName(contextName);

        entity.EntityClass = entityClass;
        entity.EntityNamespace = entityNamespace;
        entity.EntityBaseClass = entiyBaseClass;

        entity.MappingClass = mappingName;
        entity.MappingNamespace = mappingNamespace;

        entity.ContextProperty = contextName;

        entity.IsView = tableSchema is DatabaseView;

        bool? isTemporal = tableSchema[SqlServerAnnotationNames.IsTemporal] as bool?;
        if (isTemporal == true && _options.Data.Mapping.Temporal)
        {
            entity.TemporalTableName = tableSchema[SqlServerAnnotationNames.TemporalHistoryTableName] as string;
            entity.TemporalTableSchema = tableSchema[SqlServerAnnotationNames.TemporalHistoryTableSchema] as string;

            entity.TemporalStartProperty = tableSchema[SqlServerAnnotationNames.TemporalPeriodStartPropertyName] as string;

            entity.TemporalStartColumn = tableSchema[SqlServerAnnotationNames.TemporalPeriodStartColumnName] as string
                ?? entity.TemporalStartProperty;

            entity.TemporalEndProperty = tableSchema[SqlServerAnnotationNames.TemporalPeriodEndPropertyName] as string;

            entity.TemporalEndColumn = tableSchema[SqlServerAnnotationNames.TemporalPeriodEndColumnName] as string
                ?? entity.TemporalEndProperty;
        }

        entityContext.Entities.Add(entity);

        return entity;
    }


    private void CreateProperties(Entity entity, DatabaseTable tableSchema)
    {
        var columns = tableSchema.Columns;
        foreach (var column in columns)
        {
            var table = column.Table;
            if (IsIgnored(column, _options.Database.Exclude.Columns))
            {
                _logger.LogDebug("  Skipping Column : {Schema}.{Table}.{Column}", table.Schema, table.Name, column.Name);
                continue;
            }

            var mapping = column.StoreType.HasValue() ? _typeMapper.FindMapping(column.StoreType) : null;
            if (mapping == null)
            {
                _logger.LogWarning("Failed to map type {storeType} for {column}.", column.StoreType, column.Name);
                continue;
            }

            var property = entity.Properties.ByColumn(column.Name);

            if (property == null)
            {
                property = new Property
                {
                    Entity = entity,
                    ColumnName = column.Name
                };
                entity.Properties.Add(property);
            }

            string name = ToPropertyName(entity.EntityClass, column.Name);
            string propertyName = name;

            foreach (var selection in _options.Data.Entity.Renaming.Properties.Where(p => p.Expression.HasValue()))
            {
                if (selection.Expression.IsNullOrEmpty())
                    continue;

                propertyName = Regex.Replace(propertyName, selection.Expression, string.Empty);
            }

            // make sure regex doesn't remove everything
            if (propertyName.IsNullOrEmpty())
                propertyName = name;

            propertyName = _namer.UniqueName(entity.EntityClass, propertyName);

            property.PropertyName = propertyName;

            property.IsNullable = column.IsNullable;

            property.IsRowVersion = column.IsRowVersion();
            property.IsConcurrencyToken = (bool?)column[ScaffoldingAnnotationNames.ConcurrencyToken] == true;

            property.IsPrimaryKey = table.PrimaryKey?.Columns.Contains(column) == true;
            property.IsForeignKey = table.ForeignKeys.Any(c => c.Columns.Contains(column));

            property.IsUnique = table.UniqueConstraints.Any(c => c.Columns.Contains(column))
                                || table.Indexes.Where(i => i.IsUnique).Any(c => c.Columns.Contains(column));

            property.DefaultValue = column.DefaultValue;
            property.Default = column.DefaultValueSql;

            property.ValueGenerated = column.ValueGenerated;

            if (property.ValueGenerated == null && !string.IsNullOrWhiteSpace(column.ComputedColumnSql))
                property.ValueGenerated = ValueGenerated.OnAddOrUpdate;

            property.StoreType = mapping.StoreType;
            property.NativeType = mapping.StoreTypeNameBase;
            property.DataType = mapping.DbType ?? DbType.AnsiString;
            property.SystemType = mapping.ClrType;
            property.Size = mapping.Size;

            // overwrite row version type
            if (property.IsRowVersion == true && _options.Data.Mapping.RowVersion != RowVersionMapping.ByteArray && property.SystemType == typeof(byte[]))
            {
                property.SystemType = _options.Data.Mapping.RowVersion switch
                {
                    RowVersionMapping.ByteArray => typeof(byte[]),
                    RowVersionMapping.Long => typeof(long),
                    RowVersionMapping.ULong => typeof(ulong),
                    _ => typeof(byte[])
                };
            }

            property.IsProcessed = true;
        }

        entity.Properties.IsProcessed = true;

        bool? isTemporal = tableSchema[SqlServerAnnotationNames.IsTemporal] as bool?;
        if (isTemporal != true || _options.Data.Mapping.Temporal)
            return;

        // add temporal period columns
        var temporalStartColumn = tableSchema[SqlServerAnnotationNames.TemporalPeriodStartColumnName] as string
            ?? tableSchema[SqlServerAnnotationNames.TemporalPeriodStartPropertyName] as string;

        var temporalEndColumn = tableSchema[SqlServerAnnotationNames.TemporalPeriodEndColumnName] as string
            ?? tableSchema[SqlServerAnnotationNames.TemporalPeriodEndPropertyName] as string;

        if (temporalStartColumn.IsNullOrEmpty() || temporalEndColumn.IsNullOrEmpty())
            return;

        var temporalStart = entity.Properties.ByColumn(temporalStartColumn);

        if (temporalStart == null)
        {
            temporalStart = new Property { Entity = entity, ColumnName = temporalStartColumn };
            entity.Properties.Add(temporalStart);
        }

        temporalStart.PropertyName = ToPropertyName(entity.EntityClass, temporalStartColumn);
        temporalStart.ValueGenerated = ValueGenerated.OnAddOrUpdate;
        temporalStart.StoreType = "datetime2";
        temporalStart.DataType = DbType.DateTime2;
        temporalStart.SystemType = typeof(DateTime);

        temporalStart.IsProcessed = true;

        var temporalEnd = entity.Properties.ByColumn(temporalEndColumn);

        if (temporalEnd == null)
        {
            temporalEnd = new Property { Entity = entity, ColumnName = temporalEndColumn };
            entity.Properties.Add(temporalEnd);
        }

        temporalEnd.PropertyName = ToPropertyName(entity.EntityClass, temporalEndColumn);
        temporalEnd.ValueGenerated = ValueGenerated.OnAddOrUpdate;
        temporalEnd.StoreType = "datetime2";
        temporalEnd.DataType = DbType.DateTime2;
        temporalEnd.SystemType = typeof(DateTime);

        temporalEnd.IsProcessed = true;
    }


    private void CreateRelationships(EntityContext entityContext, Entity entity, DatabaseTable tableSchema)
    {
        foreach (var foreignKey in tableSchema.ForeignKeys)
        {
            // skip relationship if principal table is ignored
            if (IsIgnored(foreignKey.PrincipalTable, _options.Database.Exclude.Tables))
            {
                _logger.LogDebug("  Skipping Relationship : {name}", foreignKey.Name);
                continue;
            }

            CreateRelationship(entityContext, entity, foreignKey);
        }

        entity.Relationships.IsProcessed = true;
    }

    private void CreateRelationship(EntityContext entityContext, Entity foreignEntity, DatabaseForeignKey tableKeySchema)
    {
        Entity primaryEntity = GetEntity(entityContext, tableKeySchema.PrincipalTable, false, false);

        var primaryName = primaryEntity.EntityClass;
        var foreignName = foreignEntity.EntityClass;

        var foreignMembers = GetKeyMembers(foreignEntity, tableKeySchema.Columns, tableKeySchema.Name);
        bool foreignMembersRequired = foreignMembers.Any(c => c.IsRequired);

        var primaryMembers = GetKeyMembers(primaryEntity, tableKeySchema.PrincipalColumns, tableKeySchema.Name);
        bool primaryMembersRequired = primaryMembers.Any(c => c.IsRequired);

        // skip invalid fkeys
        if (foreignMembers.Count == 0 || primaryMembers.Count == 0)
            return;

        var relationshipName = tableKeySchema.Name;

        // ensure relationship name for sync support
        if (relationshipName.IsNullOrEmpty())
            relationshipName = $"FK_{foreignName}_{primaryName}_{primaryMembers.Select(p => p.PropertyName).ToDelimitedString("_")}";

        relationshipName = _namer.UniqueRelationshipName(relationshipName);

        var foreignRelationship = foreignEntity.Relationships
            .FirstOrDefault(r => r.RelationshipName == relationshipName && r.IsForeignKey);

        if (foreignRelationship == null)
        {
            foreignRelationship = new Relationship { RelationshipName = relationshipName };
            foreignEntity.Relationships.Add(foreignRelationship);
        }
        foreignRelationship.IsMapped = true;
        foreignRelationship.IsForeignKey = true;
        foreignRelationship.Cardinality = foreignMembersRequired ? Cardinality.One : Cardinality.ZeroOrOne;

        foreignRelationship.PrimaryEntity = primaryEntity;
        foreignRelationship.PrimaryProperties = [.. primaryMembers];

        foreignRelationship.Entity = foreignEntity;
        foreignRelationship.Properties = [.. foreignMembers];

        string prefix = GetMemberPrefix(foreignRelationship, primaryName, foreignName);

        string foreignPropertyName = ToPropertyName(foreignEntity.EntityClass, prefix + primaryName);
        foreignPropertyName = _namer.UniqueName(foreignEntity.EntityClass, foreignPropertyName);
        foreignRelationship.PropertyName = foreignPropertyName;

        // add reverse
        var primaryRelationship = primaryEntity.Relationships
            .FirstOrDefault(r => r.RelationshipName == relationshipName && !r.IsForeignKey);

        if (primaryRelationship == null)
        {
            primaryRelationship = new Relationship { RelationshipName = relationshipName };
            primaryEntity.Relationships.Add(primaryRelationship);
        }

        primaryRelationship.IsMapped = false;
        primaryRelationship.IsForeignKey = false;

        primaryRelationship.PrimaryEntity = foreignEntity;
        primaryRelationship.PrimaryProperties = [.. foreignMembers];

        primaryRelationship.Entity = primaryEntity;
        primaryRelationship.Properties = [.. primaryMembers];

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


    private static void CreateMethods(Entity entity, DatabaseTable tableSchema)
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

    private static void GetForeignKeyMethods(Entity entity, DatabaseTable table)
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

    private static void GetIndexMethods(Entity entity, DatabaseTable table)
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

    private static Method? GetMethodFromColumns(Entity entity, IEnumerable<DatabaseColumn> columns)
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


    private void GetModels(Entity? entity)
    {
        if (entity == null || entity.Models.IsProcessed)
            return;

        _options.Variables.Set(entity);

        if (_options.Model.Read.Generate)
            CreateModel(entity, _options.Model.Read, ModelType.Read);
        if (_options.Model.Create.Generate)
            CreateModel(entity, _options.Model.Create, ModelType.Create);
        if (_options.Model.Update.Generate)
            CreateModel(entity, _options.Model.Update, ModelType.Update);

        if (entity.Models.Count > 0)
        {
            var mapperNamespace = _options.Model.Mapper.Namespace ?? "Data.Mapper";

            var mapperClass = ToLegalName(_options.Model.Mapper.Name);
            mapperClass = _namer.UniqueModelName(mapperNamespace, mapperClass);

            entity.MapperClass = mapperClass;
            entity.MapperNamespace = mapperNamespace;
            entity.MapperBaseClass = _options.Model.Mapper.BaseClass;
        }

        _options.Variables.Remove(entity);

        entity.Models.IsProcessed = true;
    }

    private void CreateModel<TOption>(Entity entity, TOption options, ModelType modelType)
        where TOption : ModelOptionsBase
    {
        if (IsIgnored(entity, options, _options.Model.Shared))
            return;

        var modelNamespace = options.Namespace.HasValue()
            ? options.Namespace
            : _options.Model.Shared.Namespace;

        modelNamespace ??= "Data.Models";

        var modelClass = ToLegalName(options.Name);
        modelClass = _namer.UniqueModelName(modelNamespace, modelClass);

        var model = new Model
        {
            Entity = entity,
            ModelType = modelType,
            ModelBaseClass = options.BaseClass,
            ModelNamespace = modelNamespace,
            ModelClass = modelClass,
            ModelAttributes = options.Attributes,
        };

        foreach (var property in entity.Properties)
        {
            if (IsIgnored(property, options, _options.Model.Shared))
                continue;

            model.Properties.Add(property);
        }

        _options.Variables.Set(model);

        var validatorNamespace = _options.Model.Validator.Namespace ?? "Data.Validation";
        var validatorClass = ToLegalName(_options.Model.Validator.Name);
        validatorClass = _namer.UniqueModelName(validatorNamespace, validatorClass);

        model.ValidatorBaseClass = _options.Model.Validator.BaseClass;
        model.ValidatorClass = validatorClass;
        model.ValidatorNamespace = validatorNamespace;

        entity.Models.Add(model);

        _options.Variables.Remove(model);
    }


    private List<Property> GetKeyMembers(Entity entity, IEnumerable<DatabaseColumn> members, string? relationshipName)
    {
        var keyMembers = new List<Property>();

        foreach (var member in members)
        {
            var property = entity.Properties.ByColumn(member.Name);

            if (property == null)
                _logger.LogWarning("Could not find column {columnName} for relationship {relationshipName}.", member.Name, relationshipName);
            else
                keyMembers.Add(property);
        }

        return keyMembers;
    }

    private static string GetMemberPrefix(Relationship relationship, string primaryClass, string foreignClass)
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
        prefix = IdSuffixRegex().Replace(prefix, "");
        prefix = DigitPrefixRegex().Replace(prefix, "");

        return prefix;
    }

    private static bool IsOneToOne(DatabaseForeignKey tableKeySchema, Relationship foreignRelationship)
    {
        var foreignColumn = foreignRelationship.Properties
            .Select(p => p.ColumnName)
            .FirstOrDefault();

        return tableKeySchema.PrincipalTable.PrimaryKey != null
            && tableKeySchema.Table.PrimaryKey != null
            && tableKeySchema.Table.PrimaryKey.Columns.Count == 1
            && tableKeySchema.Table.PrimaryKey.Columns.Any(c => c.Name == foreignColumn);

        // if f.key is unique
        //return tableKeySchema.ForeignKeyMemberColumns.All(column => column.IsUnique);
    }


    private string RelationshipName(string name)
    {
        var naming = _options.Data.Entity.RelationshipNaming;
        if (naming == RelationshipNaming.Preserve)
            return name;

        if (naming == RelationshipNaming.Suffix)
            return name + "List";

        return name.Pluralize(false);
    }

    private string ContextName(string name)
    {
        var naming = _options.Data.Context.PropertyNaming;
        if (naming == ContextNaming.Preserve)
            return name;

        if (naming == ContextNaming.Suffix)
            return name + "DataSet";

        return name.Pluralize(false);
    }

    private string EntityName(string name)
    {
        var tableNaming = _options.Database.TableNaming;
        var entityNaming = _options.Data.Entity.EntityNaming;

        if (tableNaming != TableNaming.Plural && entityNaming == EntityNaming.Plural)
            name = name.Pluralize(false);
        else if (tableNaming != TableNaming.Singular && entityNaming == EntityNaming.Singular)
            name = name.Singularize(false);

        var rename = name;
        foreach (var selection in _options.Data.Entity.Renaming.Entities.Where(p => p.Expression.HasValue()))
        {
            if (selection.Expression.IsNullOrEmpty())
                continue;

            rename = Regex.Replace(rename, selection.Expression, string.Empty);
        }

        // make sure regex doesn't remove everything
        return rename.HasValue() ? rename : name;
    }


    private string ToClassName(string tableName, string? tableSchema)
    {
        tableName = EntityName(tableName);
        var className = tableName;

        if (_options.Data.Entity.PrefixWithSchemaName && tableSchema != null)
            className = $"{tableSchema}{tableName}";

        return ToLegalName(className);
    }

    private string ToPropertyName(string className, string name)
    {
        string propertyName = ToLegalName(name);
        if (className.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
            propertyName += "Member";

        return propertyName;
    }

    private static string ToLegalName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        string legalName = name;

        // remove invalid leading
        var expression = LeadingNonAlphaRegex();
        if (expression.IsMatch(name))
            legalName = expression.Replace(legalName, string.Empty);

        // prefix with column when all characters removed
        if (legalName.IsNullOrWhiteSpace())
            legalName = "Number" + name;

        return legalName.ToPascalCase();
    }


    private static bool IsIgnored(DatabaseTable table, IEnumerable<MatchOptions> exclude)
    {
        var name = $"{table.Schema}.{table.Name}";
        var includeExpressions = Enumerable.Empty<MatchOptions>();
        var excludeExpressions = exclude ?? [];

        return IsIgnored(name, excludeExpressions, includeExpressions);
    }

    private static bool IsIgnored(DatabaseColumn column, IEnumerable<MatchOptions> exclude)
    {
        var table = column.Table;
        var name = $"{table.Schema}.{table.Name}.{column.Name}";
        var includeExpressions = Enumerable.Empty<MatchOptions>();
        var excludeExpressions = exclude ?? [];

        return IsIgnored(name, excludeExpressions, includeExpressions);
    }

    private static bool IsIgnored<TOption>(Property property, TOption options, SharedModelOptions sharedOptions)
        where TOption : ModelOptionsBase
    {
        var name = $"{property.Entity.EntityClass}.{property.PropertyName}";

        var includeExpressions = new HashSet<MatchOptions>(sharedOptions?.Include?.Properties ?? []);
        var excludeExpressions = new HashSet<MatchOptions>(sharedOptions?.Exclude?.Properties ?? []);

        var includeProperties = options?.Include?.Properties ?? [];
        foreach (var expression in includeProperties)
            includeExpressions.Add(expression);

        var excludeProperties = options?.Exclude?.Properties ?? [];
        foreach (var expression in excludeProperties)
            excludeExpressions.Add(expression);

        return IsIgnored(name, excludeExpressions, includeExpressions);
    }

    private static bool IsIgnored<TOption>(Entity entity, TOption options, SharedModelOptions sharedOptions)
        where TOption : ModelOptionsBase
    {
        var name = entity.EntityClass;

        var includeExpressions = new HashSet<MatchOptions>(sharedOptions?.Include?.Entities ?? []);
        var excludeExpressions = new HashSet<MatchOptions>(sharedOptions?.Exclude?.Entities ?? []);

        var includeEntities = options?.Include?.Entities ?? [];
        foreach (var expression in includeEntities)
            includeExpressions.Add(expression);

        var excludeEntities = options?.Exclude?.Entities ?? [];
        foreach (var expression in excludeEntities)
            excludeExpressions.Add(expression);

        return IsIgnored(name, excludeExpressions, includeExpressions);
    }

    private static bool IsIgnored(string name, IEnumerable<MatchOptions> excludeExpressions, IEnumerable<MatchOptions> includeExpressions)
    {
        foreach (var expression in includeExpressions)
        {
            if (expression.IsMatch(name))
                return false;
        }

        foreach (var expression in excludeExpressions)
        {
            if (expression.IsMatch(name))
                return true;
        }

        return false;
    }


    [GeneratedRegex(@"^[^a-zA-Z_]+")]
    private static partial Regex LeadingNonAlphaRegex();

    [GeneratedRegex(@"(_ID|_id|_Id|\.ID|\.id|\.Id|ID|Id)$")]
    private static partial Regex IdSuffixRegex();

    [GeneratedRegex(@"^\d")]
    private static partial Regex DigitPrefixRegex();
}
