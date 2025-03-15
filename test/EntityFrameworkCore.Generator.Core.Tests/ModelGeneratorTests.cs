using System.Linq;

using EntityFrameworkCore.Generator.Options;

using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging.Abstractions;

using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests;

public class ModelGeneratorTests
{
    [Fact]
    public void GenerateCheckNames()
    {
        var generatorOptions = new GeneratorOptions();
        var databaseModel = new DatabaseModel
        {
            DatabaseName = "TestDatabase",
            DefaultSchema = "dbo"
        };
        var testTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "TestTable",
            Schema = "dbo"
        };
        databaseModel.Tables.Add(testTable);

        var identifierColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        testTable.Columns.Add(identifierColumn);

        var nameColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Name",
            IsNullable = true,
            StoreType = "varchar(50)"
        };
        testTable.Columns.Add(nameColumn);
        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var typeMappingSource = CreateTypeMappingSource();

        var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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

        var databaseModel = new DatabaseModel
        {
            DatabaseName = "TestDatabase",
            DefaultSchema = "dbo"
        };
        var testTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "TestTable",
            Schema = "dbo"
        };
        databaseModel.Tables.Add(testTable);

        var identifierColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        testTable.Columns.Add(identifierColumn);

        var nameColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Name",
            IsNullable = true,
            StoreType = "varchar(50)"
        };
        testTable.Columns.Add(nameColumn);
        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var typeMappingSource = CreateTypeMappingSource();

        var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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
    public void GenerateWithSymbolInDatabaseName()
    {
        var generatorOptions = new GeneratorOptions();
        var databaseModel = new DatabaseModel
        {
            DatabaseName = "Test+Symbol",
            DefaultSchema = "dbo"
        };
        var databaseTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "Test+Error",
            Schema = "dbo"
        };
        databaseModel.Tables.Add(databaseTable);

        var databaseColumn = new DatabaseColumn
        {
            Table = databaseTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        databaseTable.Columns.Add(databaseColumn);

        var generator = new ModelGenerator(NullLoggerFactory.Instance);
        var typeMappingSource = CreateTypeMappingSource();

        var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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
        var databaseModel = new DatabaseModel
        {
            DatabaseName = "TestDatabase",
            DefaultSchema = "dbo"
        };
        var testTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "TestTable",
            Schema = "dbo"
        };
        databaseModel.Tables.Add(testTable);

        var identifierColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        testTable.Columns.Add(identifierColumn);

        var numberColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "404",
            IsNullable = true,
            StoreType = "int"
        };
        testTable.Columns.Add(numberColumn);
        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var typeMappingSource = CreateTypeMappingSource();

        var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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
        var databaseModel = new DatabaseModel
        {
            DatabaseName = "TestDatabase",
            DefaultSchema = "dbo"
        };
        var testTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "TestTable",
            Schema = "dbo"
        };
        databaseModel.Tables.Add(testTable);

        var identifierColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        testTable.Columns.Add(identifierColumn);

        var nameColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Name",
            IsNullable = true,
            StoreType = "varchar(50)",
            DefaultValueSql = @"/****** Object:  Default dbo.abc0    Script Date: 4/11/99 12:35:41 PM ******/
create default abc0 as 0
"
        };
        testTable.Columns.Add(nameColumn);
        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var typeMappingSource = CreateTypeMappingSource();

        var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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
        var databaseModel = new DatabaseModel
        {
            DatabaseName = "TestDatabase",
            DefaultSchema = "dbo"
        };
        var testTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "aammstest",
            Schema = "dbo"
        };
        databaseModel.Tables.Add(testTable);

        var identifierColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        testTable.Columns.Add(identifierColumn);

        var nameColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Name",
            IsNullable = true,
            StoreType = "varchar(50)"
        };
        testTable.Columns.Add(nameColumn);
        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var typeMappingSource = CreateTypeMappingSource();

        var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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
        var databaseModel = new DatabaseModel
        {
            DatabaseName = "TestDatabase",
            DefaultSchema = "dbo"
        };
        var testTableDbo = new DatabaseTable
        {
            Database = databaseModel,
            Name = "TestTable",
            Schema = "dbo"
        };
        var testTableTst = new DatabaseTable
        {
            Database = databaseModel,
            Name = "TestTable",
            Schema = "tst"
        };
        databaseModel.Tables.Add(testTableDbo);
        databaseModel.Tables.Add(testTableTst);

        var identifierColumnDbo = new DatabaseColumn
        {
            Table = testTableDbo,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        var identifierColumnTst = new DatabaseColumn
        {
            Table = testTableTst,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        testTableDbo.Columns.Add(identifierColumnDbo);
        testTableDbo.Columns.Add(identifierColumnTst);

        var nameColumnDbo = new DatabaseColumn
        {
            Table = testTableDbo,
            Name = "Name",
            IsNullable = true,
            StoreType = "varchar(50)"
        };
        var nameColumnTst = new DatabaseColumn
        {
            Table = testTableTst,
            Name = "Name",
            IsNullable = true,
            StoreType = "varchar(50)"
        };
        testTableDbo.Columns.Add(nameColumnDbo);
        testTableDbo.Columns.Add(nameColumnTst);

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var typeMappingSource = CreateTypeMappingSource();

        var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);

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
        generatorOptions.Database.Exclude.Add(new MatchOptions(generatorOptions.Variables, "option0001") { Expression = @"dbo\.ExpressionTable$" });
        generatorOptions.Database.Exclude.Add(new MatchOptions(generatorOptions.Variables, "option0002") { Exact = @"dbo.DirectTable" });
        generatorOptions.Model.Read.Generate = true;
        generatorOptions.Model.Create.Generate = true;
        generatorOptions.Model.Update.Generate = true;
        generatorOptions.Model.Validator.Generate = true;
        generatorOptions.Model.Mapper.Generate = true;

        var databaseModel = new DatabaseModel
        {
            DatabaseName = "TestDatabase",
            DefaultSchema = "dbo"
        };
        var testTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "TestTable",
            Schema = "dbo"
        };
        databaseModel.Tables.Add(testTable);

        var identifierColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        };
        testTable.Columns.Add(identifierColumn);

        var nameColumn = new DatabaseColumn
        {
            Table = testTable,
            Name = "Name",
            IsNullable = true,
            StoreType = "varchar(50)"
        };
        testTable.Columns.Add(nameColumn);

        var expressionTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "ExpressionTable",
            Schema = "dbo"
        };
        expressionTable.Columns.Add(new DatabaseColumn
        {
            Table = testTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        });
        databaseModel.Tables.Add(expressionTable);

        var directTable = new DatabaseTable
        {
            Database = databaseModel,
            Name = "DirectTable",
            Schema = "dbo"
        };
        directTable.Columns.Add(new DatabaseColumn
        {
            Table = testTable,
            Name = "Id",
            IsNullable = false,
            StoreType = "int"
        });
        databaseModel.Tables.Add(directTable);

        var generator = new ModelGenerator(NullLoggerFactory.Instance);

        var typeMappingSource = CreateTypeMappingSource();

        var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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

    private static SqlServerTypeMappingSource CreateTypeMappingSource()
    {
#pragma warning disable EF1001 // Internal EF Core API usage.
        var sqlServerTypeMappingSource = new SqlServerTypeMappingSource(
            new TypeMappingSourceDependencies(
                new ValueConverterSelector(new ValueConverterSelectorDependencies()),
                new JsonValueReaderWriterSource(new JsonValueReaderWriterSourceDependencies()),
                Enumerable.Empty<ITypeMappingSourcePlugin>()
            ),
            new RelationalTypeMappingSourceDependencies(Enumerable.Empty<IRelationalTypeMappingSourcePlugin>())
        );
#pragma warning restore EF1001 // Internal EF Core API usage.
        return sqlServerTypeMappingSource;
    }

}
