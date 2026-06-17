using System.Reflection;

using EntityFrameworkCore.Generator.Serialization;

using Microsoft.Extensions.Logging.Abstractions;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EntityFrameworkCore.Generator.Core.Tests;

public class OptionsTests
{
    private readonly ITestOutputHelper _output;

    public OptionsTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void SaveDefault()
    {
        var generatorOptions = new GeneratorModel();
        // set user secret values
        generatorOptions.Database.UserSecretsId = Guid.NewGuid().ToString();
        generatorOptions.Database.ConnectionName = "ConnectionStrings:Generator";

        // default all to generate
        generatorOptions.Data.Query.Generate = true;
        generatorOptions.Model.Read.Generate = true;
        generatorOptions.Model.Create.Generate = true;
        generatorOptions.Model.Update.Generate = true;
        generatorOptions.Model.Validator.Generate = true;
        generatorOptions.Model.Mapper.Generate = true;

        var serializer = new SerializerBuilder()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults)
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var yaml = serializer.Serialize(generatorOptions);


        _output.WriteLine(yaml);
    }

    [Fact]
    public void Load()
    {
        var serializer = new ConfigurationSerializer(NullLogger<ConfigurationSerializer>.Instance);

        var resourcePath = "EntityFrameworkCore.Generator.Core.Tests.Options.full.yaml";
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(resourcePath);
        Assert.NotNull(stream);

        using var reader = new StreamReader(stream);

        var options = serializer.Load(reader);
        Assert.NotNull(options);
    }

    [Fact]
    public void LoadTypeMapping()
    {
        const string yaml =
            """
            data:
              entity:
                typeMapping:
                  - nativeType: geometry
                    systemType: NetTopologySuite.Geometries.Geometry
                  - nativeType: dbo.StringList
                    systemType: List<string?>
            """;

        var serializer = new ConfigurationSerializer(NullLogger<ConfigurationSerializer>.Instance);
        using var reader = new StringReader(yaml);

        var model = serializer.Load(reader);

        Assert.NotNull(model);
        Assert.NotNull(model.Data.Entity.TypeMapping);
        Assert.Equal(2, model.Data.Entity.TypeMapping.Count);

        var options = OptionMapper.Map(model);

        Assert.Equal(2, options.Data.Entity.TypeMapping.Count);
        Assert.Equal("geometry", options.Data.Entity.TypeMapping[0].NativeType);
        Assert.Equal("NetTopologySuite.Geometries.Geometry", options.Data.Entity.TypeMapping[0].SystemType);
        Assert.Equal("dbo.StringList", options.Data.Entity.TypeMapping[1].NativeType);
        Assert.Equal("List<string?>", options.Data.Entity.TypeMapping[1].SystemType);
    }

}
