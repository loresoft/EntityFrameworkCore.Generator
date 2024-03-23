using System.IO;

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

    [Fact]
    public void GenerateSpatial()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Database.ConnectionString = Database.ConnectionString;

        var generator = new CodeGenerator(NullLoggerFactory.Instance);
        var result = generator.Generate(generatorOptions);

        result.Should().BeTrue();

        const string spatialTableName = "CitiesSpatial";

        var citiesSpatialEntityFile = Path.Combine(generatorOptions.Data.Entity.Directory, spatialTableName + ".cs");
        var citiesSpatialMappingFile = Path.Combine(generatorOptions.Data.Mapping.Directory, spatialTableName + "Map.cs");

        var citiesSpatialEntityContent = File.ReadAllText(citiesSpatialEntityFile);
        var citiesSpatialMappingContent = File.ReadAllText(citiesSpatialMappingFile);

        citiesSpatialEntityContent.Contains("public NetTopologySuite.Geometries.Geometry GeometryField { get; set; }").Should().BeTrue();
        citiesSpatialEntityContent.Contains("public NetTopologySuite.Geometries.Geometry GeographyField { get; set; }").Should().BeTrue();

        citiesSpatialMappingContent.Contains("builder.Property(t => t.GeometryField)" + System.Environment.NewLine +
"                .IsRequired()" + System.Environment.NewLine +
"                .HasColumnName(\"GeometryField\")" + System.Environment.NewLine +
"                .HasColumnType(\"geometry\");").Should().BeTrue();

        citiesSpatialMappingContent.Contains("builder.Property(t => t.GeographyField)" + System.Environment.NewLine +
"                .IsRequired()" + System.Environment.NewLine +
"                .HasColumnName(\"GeographyField\")" + System.Environment.NewLine +
"                .HasColumnType(\"geography\");").Should().BeTrue();

    }

}

