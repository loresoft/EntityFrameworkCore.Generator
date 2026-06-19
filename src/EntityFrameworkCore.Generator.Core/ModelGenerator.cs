using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

using Humanizer;

using Microsoft.Extensions.Logging;

using SchemaSaurus.Metadata;

using Model = EntityFrameworkCore.Generator.Metadata.Generation.Model;
using Property = EntityFrameworkCore.Generator.Metadata.Generation.Property;

namespace EntityFrameworkCore.Generator;

public partial class ModelGenerator
{
    private readonly UniqueNamer _namer;
    private readonly ILogger _logger;
    private GeneratorOptions _options = null!;

    public ModelGenerator(ILoggerFactory logger)
    {
        _logger = logger.CreateLogger<ModelGenerator>();
        _namer = new UniqueNamer();
    }

    public EntityContext Generate(GeneratorOptions options, DatabaseModel databaseModel)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(databaseModel);

        LogBuildingCodeGenerationModel(_logger, databaseModel.DatabaseName);

        _options = options;

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

        foreach (var table in databaseModel.Tables)
        {
            if (IsIgnored(table, _options.Database.Exclude.Tables))
            {
                LogSkippingRelation(_logger, "Table", table.Schema, table.Name);
                continue;
            }

            LogProcessingRelation(_logger, "Table", table.Schema, table.Name);

            _options.Variables.Set(VariableConstants.TableSchema, ToLegalName(table.Schema));
            _options.Variables.Set(VariableConstants.TableName, ToLegalName(table.Name));

            var entity = GetEntity(entityContext, table);
            GetModels(entity);
        }

        foreach (var view in databaseModel.Views)
        {
            if (IsIgnored(view, _options.Database.Exclude.Tables))
            {
                LogSkippingRelation(_logger, "View", view.Schema, view.Name);
                continue;
            }

            LogProcessingRelation(_logger, "View", view.Schema, view.Name);

            _options.Variables.Set(VariableConstants.TableSchema, ToLegalName(view.Schema));
            _options.Variables.Set(VariableConstants.TableName, ToLegalName(view.Name));

            var entity = GetEntity(entityContext, view);
            GetModels(entity);
        }

        _options.Variables.Remove(VariableConstants.TableName);
        _options.Variables.Remove(VariableConstants.TableSchema);

        return entityContext;
    }


    private Entity GetEntity(EntityContext entityContext, RelationBase relationSchema, bool processRelationships = true, bool processMethods = true)
    {
        var entity = entityContext.Entities.ByTable(relationSchema.Name, relationSchema.Schema)
            ?? CreateEntity(entityContext, relationSchema);

        if (!entity.Properties.IsProcessed)
            CreateProperties(entity, relationSchema);

        if (relationSchema is not Table tableSchema)
            return entity;

        if (processRelationships && !entity.Relationships.IsProcessed)
            CreateRelationships(entityContext, entity, tableSchema);

        if (processMethods && !entity.Methods.IsProcessed)
            CreateMethods(entity, tableSchema);

        entity.IsProcessed = true;
        return entity;
    }

    private Entity CreateEntity(EntityContext entityContext, RelationBase relationSchema)
    {
        var entity = new Entity
        {
            Context = entityContext,
            TableName = relationSchema.Name,
            TableSchema = relationSchema.Schema
        };

        // add to context
        entityContext.Entities.Add(entity);

        var entityClass = _options.Data.Entity.Name;
        if (entityClass.IsNullOrEmpty())
            entityClass = ToClassName(relationSchema);

        entityClass = _namer.UniqueClassName(entityClass);
        var pluralName = entityClass.Pluralize(false);

        var entityNamespace = _options.Data.Entity.Namespace ?? "Data.Entities";
        var entiyBaseClass = _options.Data.Entity.BaseClass;

        var mappingName = entityClass + "Map";
        mappingName = _namer.UniqueClassName(mappingName);

        var mappingNamespace = _options.Data.Mapping.Namespace ?? "Data.Mapping";

        var contextName = ContextName(entityClass);
        contextName = ToPropertyName(entityContext.ContextClass, contextName);
        contextName = _namer.UniqueContextName(contextName);

        entity.EntityClass = entityClass;
        entity.EntityPlural = pluralName;
        entity.EntityNamespace = entityNamespace;
        entity.EntityBaseClass = entiyBaseClass;

        entity.MappingClass = mappingName;
        entity.MappingNamespace = mappingNamespace;

        entity.ContextProperty = contextName;

        entity.IsView = relationSchema is View;

        if (relationSchema is not Table tableSchema)
            return entity;

        var isTemporal = tableSchema.Options.IsTemporalTable
            && tableSchema.Options.HistoryTable != null
            && tableSchema.Options.PeriodStartColumnName.HasValue()
            && tableSchema.Options.PeriodEndColumnName.HasValue();

        if (isTemporal && _options.Data.Mapping.Temporal)
        {
            entity.TemporalTableName = tableSchema.Options.HistoryTable?.Name;
            entity.TemporalTableSchema = tableSchema.Options.HistoryTable?.Schema;

            var periodStartColumnName = tableSchema.Options.PeriodStartColumnName;
            var periodEndColumnName = tableSchema.Options.PeriodEndColumnName;

            entity.TemporalStartColumn = periodStartColumnName;
            entity.TemporalEndColumn = periodEndColumnName;

            var temporalStartProperty = ToPropertyName(entity.EntityClass, periodStartColumnName!);
            temporalStartProperty = _namer.UniqueName(entity.EntityClass, temporalStartProperty);

            entity.TemporalStartProperty = temporalStartProperty;

            var temporalEndProperty = ToPropertyName(entity.EntityClass, periodEndColumnName!);
            temporalEndProperty = _namer.UniqueName(entity.EntityClass, temporalEndProperty);

            entity.TemporalEndProperty = temporalEndProperty;
        }

        return entity;
    }


    private void CreateProperties(Entity entity, RelationBase relationSchema)
    {
        var columns = relationSchema.Columns;
        foreach (var column in columns)
        {
            var parentRelation = column.Parent;
            if (parentRelation is null || IsIgnored(column, _options.Database.Exclude.Columns))
            {
                LogSkippingColumn(_logger, parentRelation?.Schema, parentRelation?.Name, column.Name);
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
            property.NativeType = column.NativeTypeName;
            property.DataType = column.DbType;
            property.SystemType = column.SystemType;
            property.SystemTypeName = GetSystemTypeName(column.NativeTypeName, column.SystemType);
            property.Size = column.MaxLength;

            property.Default = column.DefaultValueSql;
            property.DefaultValue = TryParseDefault(column.DefaultValueSql, column.SystemType);

            property.IsComputed = column.IsComputed;
            property.IsIdentity = column.IsIdentity;
            property.IsNullable = column.IsNullable;
            property.IsRowVersion = column.IsRowVersion;
            property.IsConcurrencyToken = column.IsConcurrencyToken;

            var parentTable = parentRelation as Table;
            if (parentTable != null)
            {
                property.IsPrimaryKey = parentTable.PrimaryKey?.Columns
                    .Any(c => c.ColumnName == column.Name) == true;

                property.IsForeignKey = parentTable.ForeignKeys
                    .Any(c => c.ColumnMappings.Any(col => col.DependentColumnName == column.Name));

                property.IsUnique = parentTable.UniqueConstraints.Any(c => c.Columns.Any(col => col.ColumnName == column.Name))
                                    || parentTable.Indexes.Where(i => i.IsUnique).Any(c => c.Columns.Any(col => col.ColumnName == column.Name));
            }

            // overwrite row version type
            if (property.IsRowVersion == true
                && _options.Data.Mapping.RowVersion != RowVersionMapping.ByteArray
                && property.SystemType == typeof(byte[]))
            {
                property.SystemType = _options.Data.Mapping.RowVersion switch
                {
                    RowVersionMapping.Long => typeof(long),
                    RowVersionMapping.ULong => typeof(ulong),
                    _ => typeof(byte[])
                };
                property.SystemTypeName = property.SystemType.ToType();
            }

            property.IsProcessed = true;
        }

        entity.Properties.IsProcessed = true;

        var table = relationSchema as Table;
        if (table is null)
            return;

        bool? isTemporal =   table.Options.IsTemporalTable;
        if (isTemporal != true || _options.Data.Mapping.Temporal)
            return;

        // add temporal period columns
        var temporalStartColumn = table.Options.PeriodStartColumnName;
        var temporalEndColumn = table.Options.PeriodEndColumnName;

        if (temporalStartColumn.IsNullOrEmpty() || temporalEndColumn.IsNullOrEmpty())
            return;

        var temporalStart = entity.Properties.ByColumn(temporalStartColumn);

        if (temporalStart == null)
        {
            temporalStart = new Property { Entity = entity, ColumnName = temporalStartColumn };
            entity.Properties.Add(temporalStart);
        }

        temporalStart.PropertyName = ToPropertyName(entity.EntityClass, temporalStartColumn);
        temporalStart.IsComputed = true;
        temporalStart.NativeType = "datetime2";
        temporalStart.DataType = DbType.DateTime2;
        temporalStart.SystemType = typeof(DateTime);
        temporalStart.SystemTypeName = typeof(DateTime).ToType();

        temporalStart.IsProcessed = true;

        var temporalEnd = entity.Properties.ByColumn(temporalEndColumn);

        if (temporalEnd == null)
        {
            temporalEnd = new Property { Entity = entity, ColumnName = temporalEndColumn };
            entity.Properties.Add(temporalEnd);
        }

        temporalEnd.PropertyName = ToPropertyName(entity.EntityClass, temporalEndColumn);
        temporalEnd.IsComputed = true;
        temporalEnd.NativeType = "datetime2";
        temporalEnd.DataType = DbType.DateTime2;
        temporalEnd.SystemType = typeof(DateTime);
        temporalEnd.SystemTypeName = typeof(DateTime).ToType();

        temporalEnd.IsProcessed = true;
    }


    private void CreateRelationships(EntityContext entityContext, Entity entity, Table tableSchema)
    {
        foreach (var foreignKey in tableSchema.ForeignKeys.OrderBy(fk => fk.Name))
        {
            // skip relationship if principal table is ignored
            if (IsIgnored(foreignKey.PrincipalTable, _options.Database.Exclude.Tables))
            {
                LogSkippingRelationship(_logger, foreignKey.Name);
                continue;
            }

            if (IsIgnored(foreignKey, _options.Database.Exclude.Relationships))
            {
                LogSkippingRelationship(_logger, foreignKey.Name);
                continue;
            }

            CreateRelationship(entityContext, entity, foreignKey);
        }

        entity.Relationships.IsProcessed = true;
    }

    private void CreateRelationship(EntityContext entityContext, Entity foreignEntity, ForeignKey tableKeySchema)
    {
        Entity primaryEntity = GetEntity(entityContext, tableKeySchema.PrincipalTable, false, false);

        var primaryName = primaryEntity.EntityClass;
        var foreignName = foreignEntity.EntityClass;

        var foreignMembers = GetKeyMembers(
            foreignEntity,
            tableKeySchema.ColumnMappings.Select(c => c.DependentColumn?.Name ?? c.DependentColumnName),
            tableKeySchema.Name
        );
        bool foreignMembersRequired = foreignMembers.Any(c => c.IsRequired);

        var primaryMembers = GetKeyMembers(
            primaryEntity,
            tableKeySchema.ColumnMappings.Select(c => c.PrincipalColumn?.Name ?? c.PrincipalColumnName),
            tableKeySchema.Name
        );
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


    private static void CreateMethods(Entity entity, Table tableSchema)
    {
        if (tableSchema.PrimaryKey != null)
        {
            var method = GetMethodFromColumns(entity, tableSchema.PrimaryKey.Columns.Select(c => c.Column));
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

    private static void GetForeignKeyMethods(Entity entity, Table table)
    {
        var columnNames = new List<string?>();

        foreach (var columnName in table.ForeignKeys.SelectMany(c => c.ColumnMappings.Select(m => m.DependentColumn?.Name ?? m.DependentColumnName)))
        {
            columnNames.Add(columnName);

            var method = GetMethodFromColumnNames(entity, columnNames);
            if (method != null && entity.Methods.All(m => m.NameSuffix != method.NameSuffix))
                entity.Methods.Add(method);

            columnNames.Clear();
        }
    }

    private static void GetIndexMethods(Entity entity, Table table)
    {
        foreach (var index in table.Indexes)
        {
            var method = GetMethodFromColumns(entity, index.Columns.Select(c => c.Column));
            if (method == null)
                continue;

            method.SourceName = index.Name;
            method.IsUnique = index.IsUnique;
            method.IsIndex = true;

            if (entity.Methods.All(m => m.NameSuffix != method.NameSuffix))
                entity.Methods.Add(method);
        }
    }

    private static Method? GetMethodFromColumns(Entity entity, IEnumerable<Column?> columns)
    {
        return GetMethodFromColumnNames(entity, columns.Select(column => column?.Name));
    }

    private static Method? GetMethodFromColumnNames(Entity entity, IEnumerable<string?> columnNames)
    {
        var method = new Method { Entity = entity };
        var methodName = new StringBuilder();

        foreach (var columnName in columnNames)
        {
            if (string.IsNullOrEmpty(columnName))
                continue;

            var property = entity.Properties.ByColumn(columnName);
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
        if (!entity.IsView && _options.Model.Create.Generate)
            CreateModel(entity, _options.Model.Create, ModelType.Create);
        if (!entity.IsView && _options.Model.Update.Generate)
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

        var modelHeader = options.Header.HasValue()
            ? options.Header
            : _options.Model.Shared.Header;

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
            ModelHeader = modelHeader
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


    private List<Property> GetKeyMembers(Entity entity, IEnumerable<string?> memberNames, string? relationshipName)
    {
        var keyMembers = new List<Property>();

        foreach (var memberName in memberNames)
        {
            if (string.IsNullOrEmpty(memberName))
            {
                LogCouldNotResolveColumnName(_logger, relationshipName);
                continue;
            }

            var property = entity.Properties.ByColumn(memberName);

            if (property == null)
                LogCouldNotFindColumn(_logger, memberName, relationshipName);
            else
                keyMembers.Add(property);
        }

        return keyMembers;
    }

    private string GetSystemTypeName(string? nativeType, Type systemType)
    {
        var mapping = _options.Data.Entity.TypeMapping
            .FirstOrDefault(m => string.Equals(m.NativeType, nativeType, StringComparison.OrdinalIgnoreCase));

        if (mapping?.SystemType.HasValue() == true)
            return mapping.SystemType;

        return systemType.ToType();
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

    private static bool IsOneToOne(ForeignKey tableKeySchema, Relationship foreignRelationship)
    {
        var foreignColumn = foreignRelationship.Properties
            .Select(p => p.ColumnName)
            .FirstOrDefault();

        return tableKeySchema.PrincipalTable.PrimaryKey != null
            && tableKeySchema.DependentTable.PrimaryKey != null
            && tableKeySchema.DependentTable.PrimaryKey.Columns.Count == 1
            && tableKeySchema.DependentTable.PrimaryKey.Columns.Any(c => c.ColumnName == foreignColumn);

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

    private string ToClassName(RelationBase tableSchema)
    {
        return ToClassName(
            tableSchema.QualifiedName.Name,
            tableSchema.QualifiedName.Schema
        );
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


    private static object? TryParseDefault(string? defaultValueSql, Type type)
    {
        defaultValueSql = defaultValueSql?.Trim();
        if (string.IsNullOrEmpty(defaultValueSql))
            return null;

        // unwrap parentheses
        Unwrap();

        if (defaultValueSql.StartsWith("CONVERT", StringComparison.OrdinalIgnoreCase))
        {
            // extract value from CONVERT statement
            defaultValueSql = defaultValueSql[(defaultValueSql.IndexOf(',') + 1)..];
            defaultValueSql = defaultValueSql[..defaultValueSql.LastIndexOf(')')];

            // unwrap parentheses again
            Unwrap();
        }

        // handle NULL default
        if (defaultValueSql.Equals("NULL", StringComparison.OrdinalIgnoreCase))
            return null;

        // handle boolean defaults represented as 0 or 1
        if (type == typeof(bool) && int.TryParse(defaultValueSql, out var intValue))
            return intValue != 0;

        // handle numeric types
        if (type.IsNumeric())
        {
            try
            {
                return Convert.ChangeType(defaultValueSql, type, CultureInfo.InvariantCulture);
            }
            catch
            {
                // Ignored
                return null;
            }
        }

        // handle string literals
        if ((defaultValueSql.StartsWith('\'') || defaultValueSql.StartsWith("N'", StringComparison.OrdinalIgnoreCase))
            && defaultValueSql.EndsWith('\''))
        {
            // extract string value from quotes
            var startIndex = defaultValueSql.IndexOf('\'');
            defaultValueSql = defaultValueSql.Substring(startIndex + 1, defaultValueSql.Length - (startIndex + 2));

            if (type == typeof(string))
                return defaultValueSql;

            if (type == typeof(bool) && bool.TryParse(defaultValueSql, out var boolValue))
                return boolValue;

            if (type == typeof(Guid) && Guid.TryParse(defaultValueSql, out var guid))
                return guid;

            if (type == typeof(DateTime) && DateTime.TryParse(defaultValueSql, out var dateTime))
                return dateTime;

            if (type == typeof(DateOnly) && DateOnly.TryParse(defaultValueSql, out var dateOnly))
                return dateOnly;

            if (type == typeof(TimeOnly) && TimeOnly.TryParse(defaultValueSql, out var timeOnly))
                return timeOnly;

            if (type == typeof(DateTimeOffset) && DateTimeOffset.TryParse(defaultValueSql, out var dateTimeOffset))
                return dateTimeOffset;
        }

        return null;

        // local function to unwrap parentheses
        void Unwrap()
        {
            while (defaultValueSql.StartsWith('(') && defaultValueSql.EndsWith(')'))
                defaultValueSql = defaultValueSql[1..^1].Trim();
        }
    }


    private static bool IsIgnored(RelationBase? relation, IEnumerable<MatchOptions> exclude)
    {
        if (relation is null)
            return true;

        var name = relation.QualifiedName;
        var includeExpressions = Enumerable.Empty<MatchOptions>();
        var excludeExpressions = exclude ?? [];

        return IsIgnored(name, excludeExpressions, includeExpressions);
    }

    private static bool IsIgnored(Column column, IEnumerable<MatchOptions> exclude)
    {
        var table = column.Parent;
        if (table == null)
            return true;

        var name = $"{table.Schema}.{table.Name}.{column.Name}";
        var includeExpressions = Enumerable.Empty<MatchOptions>();
        var excludeExpressions = exclude ?? [];

        return IsIgnored(name, excludeExpressions, includeExpressions);
    }

    private static bool IsIgnored(ForeignKey relationship, IEnumerable<MatchOptions> exclude)
    {
        var table = relationship.PrincipalTable;
        if (table == null)
            return true;

        var name = $"{table.Schema}.{table.Name}.{relationship.Name}";
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

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Building code generation model from database: {databaseName}")]
    private static partial void LogBuildingCodeGenerationModel(ILogger logger, string? databaseName);

    [LoggerMessage(EventId = 2, Level = LogLevel.Debug, Message = "  Skipping {relationType} : {schema}.{name}")]
    private static partial void LogSkippingRelation(ILogger logger, string relationType, string? schema, string name);

    [LoggerMessage(EventId = 3, Level = LogLevel.Debug, Message = "  Processing {relationType} : {schema}.{name}")]
    private static partial void LogProcessingRelation(ILogger logger, string relationType, string? schema, string name);

    [LoggerMessage(EventId = 4, Level = LogLevel.Debug, Message = "  Skipping Column : {schema}.{table}.{column}")]
    private static partial void LogSkippingColumn(ILogger logger, string? schema, string? table, string column);

    [LoggerMessage(EventId = 5, Level = LogLevel.Debug, Message = "  Skipping Relationship : {name}")]
    private static partial void LogSkippingRelationship(ILogger logger, string? name);

    [LoggerMessage(EventId = 6, Level = LogLevel.Warning, Message = "Could not resolve column name for relationship {relationshipName}.")]
    private static partial void LogCouldNotResolveColumnName(ILogger logger, string? relationshipName);

    [LoggerMessage(EventId = 7, Level = LogLevel.Warning, Message = "Could not find column {columnName} for relationship {relationshipName}.")]
    private static partial void LogCouldNotFindColumn(ILogger logger, string columnName, string? relationshipName);
}
