using EntityFrameworkCore.Generator.Options;
using FluentAssertions;
using FluentCommand.SqlServer.Tests;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace EntityFrameworkCore.Generator.Core.Tests;

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

    [Fact]
    public void Generate_Should_Work_For_Password_With_CurlyBrace()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Database.ConnectionString = Database.ConnectionString
            .Replace("Integrated Security=True", @"User ID=testuser;Password=rglna{adQP123456");//This is the user specified in Script003.Tracker.User.sql

        var generator = new CodeGenerator(NullLoggerFactory.Instance);
        var result = generator.Generate(generatorOptions);

        result.Should().BeTrue();
    }
}
