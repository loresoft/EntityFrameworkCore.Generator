using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Serialization and Deserialization for the <see cref="GeneratorOptions"/> class
    /// </summary>
    public class GeneratorOptionsSerializer
    {
        /// <summary>
        /// The options file name. Default 'generation.yml'
        /// </summary>
        public const string OptionsFileName = "generation.yml";

        /// <summary>
        /// Loads the options file using the specified <paramref name="directory"/> and <paramref name="file"/>.
        /// </summary>
        /// <param name="directory">The directory where the file is located.</param>
        /// <param name="file">The name of the options file.</param>
        /// <returns>An instance of <see cref="GeneratorOptions"/> if the file exists; otherwise <c>null</c>.</returns>
        public GeneratorOptions Load(string directory = null, string file = OptionsFileName)
        {
            if (string.IsNullOrWhiteSpace(directory))
                directory = Environment.CurrentDirectory;

            if (string.IsNullOrWhiteSpace(file))
                file = OptionsFileName;

            var path = Path.Combine(directory, file);
            if (!File.Exists(path))
                return null;

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();

            GeneratorOptions generatorOptions;
            using (var streamReader = File.OpenText(path))
                generatorOptions = deserializer.Deserialize<GeneratorOptions>(streamReader);

            return generatorOptions;
        }

        /// <summary>
        /// Saves the generator options to the specified <paramref name="directory"/> and <paramref name="file"/>.
        /// </summary>
        /// <param name="generatorOptions">The generator options to save.</param>
        /// <param name="directory">The directory where the file is located.</param>
        /// <param name="file">The name of the options file.</param>
        /// <returns>The full path of the options file.</returns>
        public string Save(GeneratorOptions generatorOptions, string directory = null, string file = OptionsFileName)
        {
            if (string.IsNullOrWhiteSpace(directory))
                directory = Environment.CurrentDirectory;

            if (string.IsNullOrWhiteSpace(file))
                file = OptionsFileName;

            var path = Path.Combine(directory, file);

            var serializer = new SerializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();

            using (var streamWriter = File.CreateText(path))
                serializer.Serialize(streamWriter, generatorOptions);

            return path;
        }
    }
}
