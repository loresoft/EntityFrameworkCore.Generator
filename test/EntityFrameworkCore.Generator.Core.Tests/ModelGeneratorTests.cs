using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.Generator.Options;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests
{
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

            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("TestTable");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("TestTable");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("TestTableMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

            firstEntity.Properties.Count.Should().Be(2);

            var identifierProperty = firstEntity.Properties.ByColumn("Id");
            identifierProperty.Should().NotBeNull();
            identifierProperty.PropertyName.Should().Be("Id");

            var nameProperty = firstEntity.Properties.ByColumn("Name");
            nameProperty.Should().NotBeNull();
            nameProperty.PropertyName.Should().Be("Name");
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

            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("TestTable");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("TestTable");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("TestTableMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");
            firstEntity.MapperClass.Should().Be("TestTableProfile");
            firstEntity.MapperNamespace.Should().Be("TestDatabase.Domain.Mapping");

            firstEntity.Properties.Count.Should().Be(2);
            firstEntity.Models.Count.Should().Be(3);

            var firstModel = firstEntity.Models[0];
            firstModel.ModelClass.Should().StartWith("TestTable");
            firstModel.ModelClass.Should().EndWith("Model");
            firstModel.ModelNamespace.Should().Be("TestDatabase.Domain.Models");
            firstModel.ValidatorClass.Should().StartWith("TestTable");
            firstModel.ValidatorClass.Should().EndWith("Validator");
            firstModel.ValidatorNamespace.Should().Be("TestDatabase.Domain.Validation");

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
                Name = "Test+Error"
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
            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestSymbolContext");
            result.ContextNamespace.Should().Be("TestSymbol.Data");

            result.Entities.Count.Should().Be(1);
            result.Entities[0].EntityClass.Should().Be("TestError");
            result.Entities[0].EntityNamespace.Should().Be("TestSymbol.Data.Entities");
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
                Name = "TestTable"
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

            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.EntityClass.Should().Be("TestTable");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.Properties.Count.Should().Be(2);

            var identifierProperty = firstEntity.Properties.ByColumn("Id");
            identifierProperty.Should().NotBeNull();
            identifierProperty.PropertyName.Should().Be("Id");

            var numberProperty = firstEntity.Properties.ByColumn("404");
            numberProperty.Should().NotBeNull();
            numberProperty.PropertyName.Should().Be("Number404");
        }
    }
}

