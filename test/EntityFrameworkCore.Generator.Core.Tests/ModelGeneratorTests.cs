using System;
using System.Data;

using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Templates;

using Microsoft.Extensions.Logging.Abstractions;

using SchemaSaurus.Metadata;
using SchemaSaurus.Metadata.Builders;

using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests;

public class ModelGeneratorTests
{
    [Fact]
    public void GenerateCheckNames()
    {
        var generatorOptions = new GeneratorOptions();
        var databaseModel = CreateDatabaseModel("TestDatabase",
            table => ConfigureTable(table, "dbo", "TestTable",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Name", true, "varchar(50)", 2)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        Assert.Equal("TestDatabaseContext", result.ContextClass);
        Assert.Equal("TestDatabase.Data", result.ContextNamespace);
        Assert.Single(result.Entities);

        var firstEntity = result.Entities[0];
        Assert.Equal("TestTable", firstEntity.TableName);
        Assert.Equal("dbo", firstEntity.TableSchema);
        Assert.Equal("TestTable", firstEntity.EntityClass);
        Assert.Equal("TestDatabase.Data.Entities", firstEntity.EntityNamespace);
        Assert.Equal("TestTableMap", firstEntity.MappingClass);
        Assert.Equal("TestDatabase.Data.Mapping", firstEntity.MappingNamespace);

        Assert.Equal(2, firstEntity.Properties.Count);

        var identifierProperty = firstEntity.Properties.ByColumn("Id");
        Assert.NotNull(identifierProperty);
        Assert.Equal("Id", identifierProperty.PropertyName);

        var nameProperty = firstEntity.Properties.ByColumn("Name");
        Assert.NotNull(nameProperty);
        Assert.Equal("Name", nameProperty.PropertyName);
    }

    [Fact]
    public void GenerateModelsCheckNames()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Model.Read.Generate = true;
        generatorOptions.Model.Create.Generate = true;
        generatorOptions.Model.Update.Generate = true;
        generatorOptions.Model.Validator.Generate = true;
        generatorOptions.Model.Mapper.Generate = true;

        var databaseModel = CreateDatabaseModel("TestDatabase",
            table => ConfigureTable(table, "dbo", "TestTable",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Name", true, "varchar(50)", 2)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        Assert.Equal("TestDatabaseContext", result.ContextClass);
        Assert.Equal("TestDatabase.Data", result.ContextNamespace);
        Assert.Single(result.Entities);

        var firstEntity = result.Entities[0];
        Assert.Equal("TestTable", firstEntity.TableName);
        Assert.Equal("dbo", firstEntity.TableSchema);
        Assert.Equal("TestTable", firstEntity.EntityClass);
        Assert.Equal("TestDatabase.Data.Entities", firstEntity.EntityNamespace);
        Assert.Equal("TestTableMap", firstEntity.MappingClass);
        Assert.Equal("TestDatabase.Data.Mapping", firstEntity.MappingNamespace);
        Assert.Equal("TestTableProfile", firstEntity.MapperClass);
        Assert.Equal("TestDatabase.Domain.Mapping", firstEntity.MapperNamespace);

        Assert.Equal(2, firstEntity.Properties.Count);
        Assert.Equal(3, firstEntity.Models.Count);

        var firstModel = firstEntity.Models[0];
        Assert.StartsWith("TestTable", firstModel.ModelClass);
        Assert.EndsWith("Model", firstModel.ModelClass);
        Assert.Equal("TestDatabase.Domain.Models", firstModel.ModelNamespace);
        Assert.StartsWith("TestTable", firstModel.ValidatorClass);
        Assert.EndsWith("Validator", firstModel.ValidatorClass);
        Assert.Equal("TestDatabase.Domain.Validation", firstModel.ValidatorNamespace);

    }

    [Fact]
    public void GenerateViewModelsSkipsCreateAndUpdateModels()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Model.Read.Generate = true;
        generatorOptions.Model.Create.Generate = true;
        generatorOptions.Model.Update.Generate = true;

        var databaseModel = new DatabaseModelBuilder()
            .WithProvider("SqlServer")
            .WithDatabaseName("TestDatabase")
            .WithDefaultSchemaName("dbo")
            .AddView(view => ConfigureView(view, "dbo", "TestView",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Name", true, "varchar(50)", 2)))
            .Build();

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        Assert.Single(result.Entities);

        var firstEntity = result.Entities[0];
        Assert.True(firstEntity.IsView);
        Assert.Single(firstEntity.Models);
        Assert.Equal(ModelType.Read, firstEntity.Models[0].ModelType);
    }

    [Fact]
    public void GenerateWithSymbolInDatabaseName()
    {
        var generatorOptions = new GeneratorOptions();
        var databaseModel = CreateDatabaseModel("Test+Symbol",
            table => ConfigureTable(table, "dbo", "Test+Error",
                column => ConfigureColumn(column, "Id", false, "int", 1)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        Assert.Equal("TestSymbolContext", result.ContextClass);
        Assert.Equal("TestSymbol.Data", result.ContextNamespace);

        Assert.Single(result.Entities);
        Assert.Equal("TestError", result.Entities[0].EntityClass);
        Assert.Equal("TestSymbol.Data.Entities", result.Entities[0].EntityNamespace);
    }

    [Fact]
    public void GenerateWithAllNumberColumnName()
    {
        var generatorOptions = new GeneratorOptions();
        var databaseModel = CreateDatabaseModel("TestDatabase",
            table => ConfigureTable(table, "dbo", "TestTable",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "404", true, "int", 2)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        Assert.Equal("TestDatabaseContext", result.ContextClass);
        Assert.Equal("TestDatabase.Data", result.ContextNamespace);
        Assert.Single(result.Entities);

        var firstEntity = result.Entities[0];
        Assert.Equal("TestTable", firstEntity.EntityClass);
        Assert.Equal("TestDatabase.Data.Entities", firstEntity.EntityNamespace);
        Assert.Equal(2, firstEntity.Properties.Count);

        var identifierProperty = firstEntity.Properties.ByColumn("Id");
        Assert.NotNull(identifierProperty);
        Assert.Equal("Id", identifierProperty.PropertyName);

        var numberProperty = firstEntity.Properties.ByColumn("404");
        Assert.NotNull(numberProperty);
        Assert.Equal("Number404", numberProperty.PropertyName);
    }

    [Fact]
    public void GenerateWithComplexDefaultValue()
    {
        var generatorOptions = new GeneratorOptions();
        var databaseModel = CreateDatabaseModel("TestDatabase",
            table => ConfigureTable(table, "dbo", "TestTable",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Name", true, "varchar(50)", 2)
                    .WithDefaultValueSql(
                """
                /****** Object:  Default dbo.abc0    Script Date: 4/11/99 12:35:41 PM ******/
                create default abc0 as 0
                """)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        Assert.Equal("TestDatabaseContext", result.ContextClass);
        Assert.Equal("TestDatabase.Data", result.ContextNamespace);
        Assert.Single(result.Entities);

        var firstEntity = result.Entities[0];
        Assert.Equal("TestTable", firstEntity.TableName);
        Assert.Equal("dbo", firstEntity.TableSchema);
        Assert.Equal("TestTable", firstEntity.EntityClass);
        Assert.Equal("TestDatabase.Data.Entities", firstEntity.EntityNamespace);
        Assert.Equal("TestTableMap", firstEntity.MappingClass);
        Assert.Equal("TestDatabase.Data.Mapping", firstEntity.MappingNamespace);

        Assert.Equal(2, firstEntity.Properties.Count);

        var identifierProperty = firstEntity.Properties.ByColumn("Id");
        Assert.NotNull(identifierProperty);
        Assert.Equal("Id", identifierProperty.PropertyName);

        var nameProperty = firstEntity.Properties.ByColumn("Name");
        Assert.NotNull(nameProperty);
        Assert.Equal("Name", nameProperty.PropertyName);
    }

    [Fact]
    public void GenerateCheckNameCase()
    {
        var generatorOptions = new GeneratorOptions();
        var databaseModel = CreateDatabaseModel("TestDatabase",
            table => ConfigureTable(table, "dbo", "aammstest",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Name", true, "varchar(50)", 2)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        Assert.Equal("TestDatabaseContext", result.ContextClass);
        Assert.Equal("TestDatabase.Data", result.ContextNamespace);
        Assert.Single(result.Entities);

        var firstEntity = result.Entities[0];
        Assert.Equal("aammstest", firstEntity.TableName);
        Assert.Equal("dbo", firstEntity.TableSchema);
        Assert.Equal("Aammstest", firstEntity.EntityClass);
        Assert.Equal("TestDatabase.Data.Entities", firstEntity.EntityNamespace);
        Assert.Equal("AammstestMap", firstEntity.MappingClass);
        Assert.Equal("TestDatabase.Data.Mapping", firstEntity.MappingNamespace);

        Assert.Equal(2, firstEntity.Properties.Count);

        var identifierProperty = firstEntity.Properties.ByColumn("Id");
        Assert.NotNull(identifierProperty);
        Assert.Equal("Id", identifierProperty.PropertyName);

        var nameProperty = firstEntity.Properties.ByColumn("Name");
        Assert.NotNull(nameProperty);
        Assert.Equal("Name", nameProperty.PropertyName);
    }

    [Fact]
    public void GenerateWithPrefixedSchemaName()
    {

        var generatorOptions = new GeneratorOptions();
        generatorOptions.Data.Entity.PrefixWithSchemaName = true;
        var databaseModel = CreateDatabaseModel("TestDatabase",
            table => ConfigureTable(table, "dbo", "TestTable",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Name", true, "varchar(50)", 2)),
            table => ConfigureTable(table, "tst", "TestTable",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Name", true, "varchar(50)", 2)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);

        Assert.Equal("TestDatabaseContext", result.ContextClass);
        Assert.Equal("TestDatabase.Data", result.ContextNamespace);
        Assert.Equal(2, result.Entities.Count);

        var firstEntity = result.Entities[0];
        Assert.Equal("TestTable", firstEntity.TableName);
        Assert.Equal("dbo", firstEntity.TableSchema);
        Assert.Equal("DboTestTable", firstEntity.EntityClass);
        Assert.Equal("TestDatabase.Data.Entities", firstEntity.EntityNamespace);
        Assert.Equal("DboTestTableMap", firstEntity.MappingClass);
        Assert.Equal("TestDatabase.Data.Mapping", firstEntity.MappingNamespace);

        var secondEntity = result.Entities[1];
        Assert.Equal("TestTable", secondEntity.TableName);
        Assert.Equal("tst", secondEntity.TableSchema);
        Assert.Equal("TstTestTable", secondEntity.EntityClass);
        Assert.Equal("TestDatabase.Data.Entities", secondEntity.EntityNamespace);
        Assert.Equal("TstTestTableMap", secondEntity.MappingClass);
        Assert.Equal("TestDatabase.Data.Mapping", secondEntity.MappingNamespace);

    }

    [Fact]
    public void GenerateIgnoreTable()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Database.Exclude.Tables.Add(new MatchOptions(generatorOptions.Variables, "option0001") { Expression = @"dbo\.ExpressionTable$" });
        generatorOptions.Database.Exclude.Tables.Add(new MatchOptions(generatorOptions.Variables, "option0002") { Exact = @"dbo.DirectTable" });
        generatorOptions.Model.Read.Generate = true;
        generatorOptions.Model.Create.Generate = true;
        generatorOptions.Model.Update.Generate = true;
        generatorOptions.Model.Validator.Generate = true;
        generatorOptions.Model.Mapper.Generate = true;

        var databaseModel = CreateDatabaseModel("TestDatabase",
            table => ConfigureTable(table, "dbo", "TestTable",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Name", true, "varchar(50)", 2)),
            table => ConfigureTable(table, "dbo", "ExpressionTable",
                column => ConfigureColumn(column, "Id", false, "int", 1)),
            table => ConfigureTable(table, "dbo", "DirectTable",
                column => ConfigureColumn(column, "Id", false, "int", 1)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        Assert.Equal("TestDatabaseContext", result.ContextClass);
        Assert.Equal("TestDatabase.Data", result.ContextNamespace);
        Assert.Single(result.Entities);

        var firstEntity = result.Entities[0];
        Assert.Equal("TestTable", firstEntity.TableName);
        Assert.Equal("dbo", firstEntity.TableSchema);
        Assert.Equal("TestTable", firstEntity.EntityClass);
        Assert.Equal("TestDatabase.Data.Entities", firstEntity.EntityNamespace);
        Assert.Equal("TestTableMap", firstEntity.MappingClass);
        Assert.Equal("TestDatabase.Data.Mapping", firstEntity.MappingNamespace);
        Assert.Equal("TestTableProfile", firstEntity.MapperClass);
        Assert.Equal("TestDatabase.Domain.Mapping", firstEntity.MapperNamespace);

        Assert.Equal(2, firstEntity.Properties.Count);
        Assert.Equal(3, firstEntity.Models.Count);

        var firstModel = firstEntity.Models[0];
        Assert.StartsWith("TestTable", firstModel.ModelClass);
        Assert.EndsWith("Model", firstModel.ModelClass);
        Assert.Equal("TestDatabase.Domain.Models", firstModel.ModelNamespace);
        Assert.StartsWith("TestTable", firstModel.ValidatorClass);
        Assert.EndsWith("Validator", firstModel.ValidatorClass);
        Assert.Equal("TestDatabase.Domain.Validation", firstModel.ValidatorNamespace);

    }

    [Fact]
    public void GenerateWithTypeMapping()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Project.Nullable = true;
        generatorOptions.Model.Read.Generate = true;
        generatorOptions.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = "geometry",
            SystemType = "NetTopologySuite.Geometries.Geometry"
        });
        generatorOptions.Data.Entity.TypeMapping.Add(new TypeMappingOptions
        {
            NativeType = "dbo.StringList",
            SystemType = "List<string?>"
        });

        var databaseModel = CreateDatabaseModel("TestDatabase",
            table => ConfigureTable(table, "dbo", "TestTable",
                column => ConfigureColumn(column, "Id", false, "int", 1),
                column => ConfigureColumn(column, "Geometry", false, "geometry", 2),
                column => ConfigureColumn(column, "Tags", true, "dbo.StringList", 3)));

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var result = generator.Generate(generatorOptions, databaseModel);
        var entity = Assert.Single(result.Entities);

        var geometryProperty = entity.Properties.ByColumn("Geometry");
        Assert.NotNull(geometryProperty);
        Assert.Equal("NetTopologySuite.Geometries.Geometry", geometryProperty.SystemTypeName);

        var tagsProperty = entity.Properties.ByColumn("Tags");
        Assert.NotNull(tagsProperty);
        Assert.Equal("List<string?>", tagsProperty.SystemTypeName);

        var entityCode = new EntityClassTemplate(entity, generatorOptions).WriteCode();
        Assert.Contains("public NetTopologySuite.Geometries.Geometry Geometry { get; set; } = null!;", entityCode);
        Assert.Contains("public List<string?>? Tags { get; set; }", entityCode);

        var model = Assert.Single(entity.Models);
        var modelCode = new ModelClassTemplate(model, generatorOptions).WriteCode();
        Assert.Contains("public NetTopologySuite.Geometries.Geometry Geometry { get; set; } = null!;", modelCode);
        Assert.Contains("public List<string?>? Tags { get; set; }", modelCode);
    }

    [Fact]
    public void GenerateModelXmlDocumentation()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Model.Read.Document = true;

        var entity = new Entity
        {
            EntityClass = "Order&Item",
            TableName = "Order<Items>",
            IsView = true
        };
        var model = new Model
        {
            Entity = entity,
            ModelType = ModelType.Read,
            ModelNamespace = "TestDatabase.Domain.Models",
            ModelClass = "OrderItemReadModel"
        };
        model.Properties.Add(new Property
        {
            PropertyName = "Title",
            ColumnName = "Title & Subtitle",
            SystemType = typeof(string),
            SystemTypeName = string.Empty,
            IsNullable = false
        });

        var modelCode = new ModelClassTemplate(model, generatorOptions).WriteCode();

        Assert.Contains("/// Represents a read model for the <c>Order&amp;Item</c> entity mapped to the <c>Order&lt;Items&gt;</c> view.", modelCode);
        Assert.Contains("/// Gets or sets the <c>Title</c> value mapped from the <c>Title &amp; Subtitle</c> column.", modelCode);
        Assert.Contains("/// The <c>Title</c> model value.", modelCode);
    }

    [Fact]
    public void GenerateEntityXmlDocumentation()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Data.Entity.Document = true;

        var entity = new Entity
        {
            EntityNamespace = "TestDatabase.Data.Entities",
            EntityClass = "OrderItem",
            TableSchema = "Sales&Archive",
            TableName = "Order<Items>"
        };
        var userIdProperty = new Property
        {
            PropertyName = "UserId",
            ColumnName = "User & Owner Id",
            SystemType = typeof(Guid),
            SystemTypeName = string.Empty,
            IsNullable = false
        };
        entity.Properties.Add(userIdProperty);
        entity.Relationships.Add(new Relationship
        {
            Entity = entity,
            PropertyName = "User",
            Cardinality = Cardinality.One,
            PrimaryEntity = new Entity
            {
                EntityNamespace = entity.EntityNamespace,
                EntityClass = "User"
            },
            Properties = new global::EntityFrameworkCore.Generator.Metadata.Generation.PropertyCollection([userIdProperty])
        });
        entity.Relationships.Add(new Relationship
        {
            Entity = entity,
            PropertyName = "OrderLines",
            Cardinality = Cardinality.Many,
            PrimaryEntity = new Entity
            {
                EntityNamespace = entity.EntityNamespace,
                EntityClass = "OrderLine"
            }
        });

        var entityCode = new EntityClassTemplate(entity, generatorOptions).WriteCode();

        Assert.Contains("/// Represents the <c>OrderItem</c> entity mapped to the <c>Sales&amp;Archive.Order&lt;Items&gt;</c> table.", entityCode);
        Assert.Contains("/// Initializes a new instance of the <see cref=\"OrderItem\"/> class and its collection navigation properties.", entityCode);
        Assert.Contains("/// Gets or sets the <c>UserId</c> value mapped to the <c>User &amp; Owner Id</c> column.", entityCode);
        Assert.Contains("/// The <c>UserId</c> entity value.", entityCode);
        Assert.Contains("/// Gets or sets the related <see cref=\"User\" /> entity.", entityCode);
        Assert.Contains("/// <seealso cref=\"UserId\" />", entityCode);
        Assert.Contains("/// Gets or sets the related <see cref=\"OrderLine\" /> entity collection.", entityCode);
    }

    [Fact]
    public void GenerateDataContextXmlDocumentation()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Data.Context.Document = true;

        var entityContext = new EntityContext
        {
            ContextNamespace = "TestDatabase.Data",
            ContextClass = "TestDatabaseContext",
            ContextBaseClass = "DbContext",
            DatabaseName = "Test & Reporting"
        };
        entityContext.Entities.Add(new Entity
        {
            ContextProperty = "OrderItems",
            EntityNamespace = "TestDatabase.Data.Entities",
            EntityClass = "OrderItem",
            MappingNamespace = "TestDatabase.Data.Mapping",
            MappingClass = "OrderItemMap",
            TableSchema = "Sales&Archive",
            TableName = "Order<Items>"
        });

        var contextCode = new DataContextTemplate(entityContext, generatorOptions).WriteCode();

        Assert.Contains("/// Represents a session with the <c>Test &amp; Reporting</c> database and provides access to generated entity sets.", contextCode);
        Assert.Contains("/// <param name=\"options\">The options used to configure this <see cref=\"DbContext\" /> instance.</param>", contextCode);
        Assert.Contains("/// Gets or sets the <see cref=\"DbSet{TEntity}\" /> for <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entities mapped to the <c>Sales&amp;Archive.Order&lt;Items&gt;</c> table.", contextCode);
        Assert.Contains("/// The <c>OrderItems</c> entity set.", contextCode);
        Assert.Contains("/// Configures entity mappings for the generated model.", contextCode);
        Assert.Contains("/// <param name=\"modelBuilder\">The builder used to configure the generated entity model.</param>", contextCode);
    }

    [Fact]
    public void GenerateMapperXmlDocumentation()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Model.Mapper.Document = true;

        var entity = new Entity
        {
            EntityNamespace = "TestDatabase.Data.Entities",
            EntityClass = "OrderItem",
            MapperNamespace = "TestDatabase.Domain.Mapping",
            MapperClass = "OrderItemProfile",
            TableSchema = "Sales&Archive",
            TableName = "Order<Items>"
        };
        entity.Models.Add(new Model
        {
            Entity = entity,
            ModelType = ModelType.Read,
            ModelNamespace = "TestDatabase.Domain.Models",
            ModelClass = "OrderItemReadModel"
        });
        entity.Models.Add(new Model
        {
            Entity = entity,
            ModelType = ModelType.Update,
            ModelNamespace = "TestDatabase.Domain.Models",
            ModelClass = "OrderItemUpdateModel"
        });

        var mapperCode = new MapperClassTemplate(entity, generatorOptions).WriteCode();

        Assert.Contains("/// Configures AutoMapper mappings for the <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entity mapped to the <c>Sales&amp;Archive.Order&lt;Items&gt;</c> table and its generated read and update models.", mapperCode);
        Assert.Contains("/// Initializes a new instance of the <see cref=\"TestDatabase.Domain.Mapping.OrderItemProfile\"/> class and creates mappings for <see cref=\"TestDatabase.Data.Entities.OrderItem\" />.", mapperCode);
    }

    [Fact]
    public void GenerateMappingXmlDocumentation()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Data.Mapping.Document = true;

        var entity = new Entity
        {
            EntityNamespace = "TestDatabase.Data.Entities",
            EntityClass = "OrderItem",
            MappingNamespace = "TestDatabase.Data.Mapping",
            MappingClass = "OrderItemMap",
            TableSchema = "Sales&Archive",
            TableName = "Order<Items>"
        };
        entity.Properties.Add(new Property
        {
            PropertyName = "UserId",
            ColumnName = "User & Owner Id",
            SystemType = typeof(Guid),
            SystemTypeName = string.Empty,
            IsNullable = false,
            IsPrimaryKey = true
        });

        var mappingCode = new MappingClassTemplate(entity, generatorOptions).WriteCode();

        Assert.Contains("/// Configures Entity Framework Core mapping for the <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entity mapped to the <c>Sales&amp;Archive.Order&lt;Items&gt;</c> table.", mappingCode);
        Assert.Contains("/// Configures the table, key, property, and relationship mappings for <see cref=\"TestDatabase.Data.Entities.OrderItem\" />.", mappingCode);
        Assert.Contains("/// <param name=\"builder\">The builder used to configure <see cref=\"TestDatabase.Data.Entities.OrderItem\" />.</param>", mappingCode);
        Assert.Contains("/// Contains table mapping constants for <see cref=\"TestDatabase.Data.Entities.OrderItem\" />.", mappingCode);
        Assert.Contains("/// The database schema name for <see cref=\"TestDatabase.Data.Entities.OrderItem\" />.", mappingCode);
        Assert.Contains("/// The database table name for <see cref=\"TestDatabase.Data.Entities.OrderItem\" />.", mappingCode);
        Assert.Contains("/// Contains column name constants for <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> properties.", mappingCode);
        Assert.Contains("/// The <c>User &amp; Owner Id</c> column name for <see cref=\"TestDatabase.Data.Entities.OrderItem.UserId\" />.", mappingCode);
    }

    [Fact]
    public void GenerateValidatorXmlDocumentation()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Model.Validator.Document = true;

        var entity = new Entity
        {
            EntityNamespace = "TestDatabase.Data.Entities",
            EntityClass = "OrderItem",
            TableSchema = "Sales&Archive",
            TableName = "Order<Items>"
        };
        var model = new Model
        {
            Entity = entity,
            ModelType = ModelType.Create,
            ModelNamespace = "TestDatabase.Domain.Models",
            ModelClass = "OrderItemCreateModel",
            ValidatorNamespace = "TestDatabase.Domain.Validation",
            ValidatorClass = "OrderItemCreateModelValidator"
        };

        var validatorCode = new ValidatorClassTemplate(model, generatorOptions).WriteCode();

        Assert.Contains("/// Defines FluentValidation rules for the <see cref=\"TestDatabase.Domain.Models.OrderItemCreateModel\" /> create model for the <c>OrderItem</c> entity mapped to the <c>Sales&amp;Archive.Order&lt;Items&gt;</c> table.", validatorCode);
        Assert.Contains("/// Initializes a new instance of the <see cref=\"TestDatabase.Domain.Validation.OrderItemCreateModelValidator\"/> class and configures validation rules for <see cref=\"TestDatabase.Domain.Models.OrderItemCreateModel\" />.", validatorCode);
    }

    [Fact]
    public void GenerateQueryExtensionXmlDocumentation()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Data.Query.Document = true;
        generatorOptions.Data.Query.Namespace = "TestDatabase.Data.Queries";

        var entity = new Entity
        {
            EntityNamespace = "TestDatabase.Data.Entities",
            EntityClass = "OrderItem",
            TableSchema = "Sales&Archive",
            TableName = "Order<Items>"
        };
        var userIdProperty = new Property
        {
            PropertyName = "UserId",
            ColumnName = "User & Owner Id",
            SystemType = typeof(Guid),
            SystemTypeName = string.Empty,
            IsNullable = false
        };
        entity.Properties.Add(userIdProperty);
        entity.Methods.Add(new Method
        {
            Entity = entity,
            NameSuffix = "ByUserId",
            SourceName = "IX_Order<UserId>",
            IsIndex = true,
            Properties = new global::EntityFrameworkCore.Generator.Metadata.Generation.PropertyCollection([userIdProperty])
        });
        entity.Methods.Add(new Method
        {
            Entity = entity,
            NameSuffix = "ByUserIdUnique",
            SourceName = "UX_Order<UserId>",
            IsUnique = true,
            Properties = new global::EntityFrameworkCore.Generator.Metadata.Generation.PropertyCollection([userIdProperty])
        });

        var queryCode = new QueryExtensionTemplate(entity, generatorOptions).WriteCode();

        Assert.Contains("/// Provides query extension methods for <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entities mapped to the <c>Sales&amp;Archive.Order&lt;Items&gt;</c> table.", queryCode);
        Assert.Contains("/// Filters <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entities by <c>UserId</c>.", queryCode);
        Assert.Contains("/// <param name=\"queryable\">The source query for <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entities.</param>", queryCode);
        Assert.Contains("/// <param name=\"userId\">The value to match against <see cref=\"TestDatabase.Data.Entities.OrderItem.UserId\" /> mapped to the <c>User &amp; Owner Id</c> column.</param>", queryCode);
        Assert.Contains("/// <returns>An <see cref=\"IQueryable{T}\" /> of <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entities matching the specified values.</returns>", queryCode);
        Assert.Contains("/// Gets the <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entity matching the unique index <c>UX_Order&lt;UserId&gt;</c>.", queryCode);
        Assert.Contains("/// <param name=\"cancellationToken\">A <see cref=\"CancellationToken\" /> to observe while waiting for the operation to complete.</param>", queryCode);
        Assert.Contains("/// <returns>A task that represents the asynchronous operation. The task result contains the matching <see cref=\"TestDatabase.Data.Entities.OrderItem\" /> entity, or <see langword=\"null\" /> if no match is found.</returns>", queryCode);
    }

    private static DatabaseModel CreateDatabaseModel(string databaseName, params Action<TableBuilder>[] configureTables)
    {
        var builder = new DatabaseModelBuilder()
            .WithProvider("SqlServer")
            .WithDatabaseName(databaseName)
            .WithDefaultSchemaName("dbo");

        foreach (var configureTable in configureTables)
            builder.AddTable(configureTable);

        return builder.Build();
    }

    private static TableBuilder ConfigureTable(
        TableBuilder builder,
        string schema,
        string tableName,
        params Func<ColumnBuilder, ColumnBuilder>[] configureColumns)
    {
        builder.WithQualifiedName(schema, tableName);

        foreach (var configureColumn in configureColumns)
            builder.AddColumn(column => configureColumn(column));

        return builder;
    }

    private static ViewBuilder ConfigureView(
        ViewBuilder builder,
        string schema,
        string viewName,
        params Func<ColumnBuilder, ColumnBuilder>[] configureColumns)
    {
        builder.WithQualifiedName(schema, viewName);

        foreach (var configureColumn in configureColumns)
            builder.AddColumn(column => configureColumn(column));

        return builder;
    }

    private static ColumnBuilder ConfigureColumn(
        ColumnBuilder builder,
        string name,
        bool isNullable,
        string nativeTypeName,
        int ordinalPosition)
    {
        var (dbType, systemType) = GetTypeMapping(nativeTypeName);

        return builder
            .WithName(name)
            .WithOrdinalPosition(ordinalPosition)
            .WithIsNullable(isNullable)
            .WithDbType(dbType)
            .WithNativeTypeName(nativeTypeName)
            .WithSystemType(systemType);
    }

    private static (DbType DbType, Type SystemType) GetTypeMapping(string nativeTypeName)
    {
        return nativeTypeName switch
        {
            "int" => (DbType.Int32, typeof(int)),
            "varchar(50)" => (DbType.String, typeof(string)),
            "geometry" => (DbType.Object, typeof(object)),
            "dbo.StringList" => (DbType.Object, typeof(object)),
            _ => throw new ArgumentOutOfRangeException(nameof(nativeTypeName), nativeTypeName, "Unsupported test column type.")
        };
    }

}
