using EntityFrameworkCore.Generator.Options;
using FluentAssertions;
using FluentCommand.SqlServer.Tests;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace EntityFrameworkCore.Generator.Core.Tests
{
    public class CodeGeneratorTests : DatabaseTestBase
    {
        public CodeGeneratorTests(ITestOutputHelper output, DatabaseFixture databaseFixture) : base(output, databaseFixture)
        {
        }

        [Fact]
        public void Generate()
        {
            var generatorOptions = new GeneratorOptions();
            generatorOptions.Database.ConnectionString = Database.ConnectionString;

            var generator = new CodeGenerator(NullLoggerFactory.Instance);
            var result = generator.Generate(generatorOptions);


            result.Should().BeTrue();
        }

    }
}
