using System;
using EntityFrameworkCore.Generator.Options;
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

        // null out collection for cleaner yaml file
        generatorOptions.Database.Tables = null;
        generatorOptions.Database.Schemas = null;
        generatorOptions.Model.Shared.Include = null;
        generatorOptions.Model.Shared.Exclude = null;
        generatorOptions.Model.Read.Include = null;
        generatorOptions.Model.Read.Exclude = null;
        generatorOptions.Model.Create.Include = null;
        generatorOptions.Model.Create.Exclude = null;
        generatorOptions.Model.Update.Include = null;
        generatorOptions.Model.Update.Exclude = null;

        generatorOptions.Script = null;

        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();


        var yaml = serializer.Serialize(generatorOptions);


        _output.WriteLine(yaml);
    }
}