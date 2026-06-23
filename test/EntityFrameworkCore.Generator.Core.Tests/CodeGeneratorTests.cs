using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Options;

using FluentCommand.SqlServer.Tests;

using Microsoft.Extensions.Logging.Abstractions;

namespace EntityFrameworkCore.Generator.Core.Tests;

public class CodeGeneratorTests : DatabaseTestBase
{
    public CodeGeneratorTests(ITestOutputHelper output, DatabaseFixture databaseFixture) : base(output, databaseFixture)
    {
    }

    [Fact]
    public async Task Generate()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Database.ConnectionString = Database.ConnectionString;

        var generator = new CodeGenerator(NullLoggerFactory.Instance);
        var result = await generator.GenerateAsync(generatorOptions);


        Assert.True(result);
    }

    [Fact]
    public async Task GenerateSpatial()
    {
        var generatorOptions = new GeneratorOptions();
        generatorOptions.Database.ConnectionString = Database.ConnectionString;
        generatorOptions.Data.Entity.TypeMapping.Add(
            new TypeMappingOptions
            {
                SystemType = "NetTopologySuite.Geometries.Geometry",
                NativeType = "geometry"
            }
        );
        generatorOptions.Data.Entity.TypeMapping.Add(
            new TypeMappingOptions
            {
                SystemType = "NetTopologySuite.Geometries.Geometry",
                NativeType = "geography"
            }
        );

        var generator = new CodeGenerator(NullLoggerFactory.Instance);
        var result = await generator.GenerateAsync(generatorOptions);

        Assert.True(result);

        const string spatialTableName = "CitiesSpatial";

        var entityDirectory = generatorOptions.Data.Entity.Directory;
        var mappingDirectory = generatorOptions.Data.Mapping.Directory;

        Assert.NotNull(entityDirectory);
        Assert.NotNull(mappingDirectory);

        var citiesSpatialEntityFile = Path.Combine(entityDirectory, spatialTableName + ".cs");
        var citiesSpatialMappingFile = Path.Combine(mappingDirectory, spatialTableName + "Map.cs");

        var citiesSpatialEntityContent = File.ReadAllText(citiesSpatialEntityFile);
        var citiesSpatialMappingContent = File.ReadAllText(citiesSpatialMappingFile);

        Assert.Contains("public NetTopologySuite.Geometries.Geometry GeometryField { get; set; }", citiesSpatialEntityContent);
        Assert.Contains("public NetTopologySuite.Geometries.Geometry GeographyField { get; set; }", citiesSpatialEntityContent);

        Assert.Contains("builder.Property(t => t.GeometryField)" + System.Environment.NewLine +
"                .IsRequired()" + System.Environment.NewLine +
"                .HasColumnName(\"GeometryField\")" + System.Environment.NewLine +
"                .HasColumnType(\"geometry\");", citiesSpatialMappingContent);

        Assert.Contains("builder.Property(t => t.GeographyField)" + System.Environment.NewLine +
"                .IsRequired()" + System.Environment.NewLine +
"                .HasColumnName(\"GeographyField\")" + System.Environment.NewLine +
"                .HasColumnType(\"geography\");", citiesSpatialMappingContent);

    }

    [Theory]
    [InlineData(typeof(int), "int")]
    [InlineData(typeof(bool), "bool")]
    [InlineData(typeof(byte), "byte")]
    [InlineData(typeof(Guid), "Guid")]
    [InlineData(typeof(DateTimeOffset), "DateTimeOffset")]
    [InlineData(typeof(List<int>), "List<int>")]
    [InlineData(typeof(Dictionary<int, string>), "Dictionary<int, string>")]
    [InlineData(typeof(Dictionary<int, List<string>>), "Dictionary<int, List<string>>")]
    [InlineData(typeof(List<List<string>>), "List<List<string>>")]
    [InlineData(typeof(int[]), "int[]")]
    [InlineData(typeof(string[][]), "string[][]")]
    [InlineData(typeof(System.Net.IPAddress[]), "System.Net.IPAddress[]")]
    [InlineData(typeof(System.ComponentModel.BindingList<int>), "System.ComponentModel.BindingList<int>")]
    public void ConvertToTypeString(Type type, string expected)
       => Assert.Equal(expected, type.ToType());
}
