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
        public void Generate()
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
    }
}
