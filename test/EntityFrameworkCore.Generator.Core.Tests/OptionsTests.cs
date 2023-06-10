using System;
using System.IO;
using System.Reflection;

using EntityFrameworkCore.Generator.Options;

using FluentAssertions;

using Microsoft.Extensions.Logging.Abstractions;

using Xunit;
using Xunit.Abstractions;

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
        var generatorOptions = new GeneratorOptions();
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
        using var reader = new StreamReader(stream);

        var options = serializer.Load(reader);
        options.Should().NotBeNull();
    }

}
