using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFrameworkCore.Generator.Options;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

            var typeMappingSource = CreateTypeMappingSource();

            var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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

            var typeMappingSource = CreateTypeMappingSource();

            var result = generator.Generate(generatorOptions, databaseModel, typeMappingSource);
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
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("aammstest");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("Aammstest");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("AammstestMap");
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

            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(2);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("TestTable");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("DboTestTable");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("DboTestTableMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

            var secondEntity = result.Entities[1];
            secondEntity.TableName.Should().Be("TestTable");
            secondEntity.TableSchema.Should().Be("tst");
            secondEntity.EntityClass.Should().Be("TstTestTable");
            secondEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            secondEntity.MappingClass.Should().Be("TstTestTableMap");
            secondEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

        }

        [Fact]
        public void GenerateIgnoreTable()
        {
            var generatorOptions = new GeneratorOptions();
            generatorOptions.Database.Exclude.Add(new MatchOptions { Expression = @"dbo\.ExpressionTable$" });
            generatorOptions.Database.Exclude.Add(new MatchOptions { Exact = @"dbo.DirectTable" });
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
        public void GenerateWithFilteredEntityName()
        {

            var generatorOptions = new GeneratorOptions();
            generatorOptions.Data.Entity.EntityNamingFilters = new List<EntityNamingFilter>()
            {
                new EntityNamingFilter()
                {
                    Pattern = "^tbl(?<ClassName>.*?)$",
                },
                new EntityNamingFilter()
                {
                    Pattern = "^vw_(?<ClassName>.*?)$",
                    Suffix = "View"
                }
            };
            var databaseModel = new DatabaseModel
            {
                DatabaseName = "TestDatabase",
                DefaultSchema = "dbo"
            };
            var testTableDbo = new DatabaseTable
            {
                Database = databaseModel,
                Name = "tblTest",
                Schema = "dbo"
            };
            var testTableTst = new DatabaseTable
            {
                Database = databaseModel,
                Name = "vw_Test",
                Schema = "dbo"
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

            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(2);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("tblTest");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("Test");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("TestMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

            var secondEntity = result.Entities[1];
            secondEntity.TableName.Should().Be("vw_Test");
            secondEntity.TableSchema.Should().Be("dbo");
            secondEntity.EntityClass.Should().Be("TestView");
            secondEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            secondEntity.MappingClass.Should().Be("TestViewMap");
            secondEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

        }

        private static SqlServerTypeMappingSource CreateTypeMappingSource()
        {
#pragma warning disable EF1001 // Internal EF Core API usage.
            var sqlServerTypeMappingSource = new SqlServerTypeMappingSource(
                new TypeMappingSourceDependencies(
                    new ValueConverterSelector(
                        new ValueConverterSelectorDependencies()
                    ),
                    Enumerable.Empty<ITypeMappingSourcePlugin>()
                ),
                new RelationalTypeMappingSourceDependencies(Enumerable.Empty<IRelationalTypeMappingSourcePlugin>())
            );
#pragma warning restore EF1001 // Internal EF Core API usage.
            return sqlServerTypeMappingSource;
        }

    }
}

