using System;
using EntityFrameworkCore.Generator.Options;
using Xunit;
using Xunit.Abstractions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EntityFrameworkCore.Generator.Core.Tests
{
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

            var serializer = new SerializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();


            var yaml = serializer.Serialize(generatorOptions);


            _output.WriteLine(yaml);
        }
    }
}
