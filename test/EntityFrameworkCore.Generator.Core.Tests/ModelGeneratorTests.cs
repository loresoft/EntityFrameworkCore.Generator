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
