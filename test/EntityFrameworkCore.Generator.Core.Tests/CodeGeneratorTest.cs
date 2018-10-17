using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.Generator.Options;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests
{
    public class CodeGeneratorTest
    {
        [Fact]
        public void Generate()
        {
            var generatorOptions = new GeneratorOptions();
            generatorOptions.Database.ConnectionString = "Data Source=(local);Initial Catalog=Tracker;Integrated Security=True";
            generatorOptions.Database.Name = "Tracker";

            var generator = new CodeGenerator(NullLoggerFactory.Instance);
            var result = generator.Generate(generatorOptions);


            result.Should().BeTrue();
        }
    }
}
