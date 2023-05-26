using System;
using System.IO;

using EntityFrameworkCore.Generator.Serialization;

using Microsoft.Extensions.Logging;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EntityFrameworkCore.Generator;

/// <summary>
/// Serialization and Deserialization for the <see cref="Generator"/> class
/// </summary>
public class ConfigurationSerializer : IConfigurationSerializer
{
    private readonly ILogger<ConfigurationSerializer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationSerializer"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ConfigurationSerializer(ILogger<ConfigurationSerializer> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// The options file name. Default 'generation.yml'
    /// </summary>
    public const string OptionsFileName = "generation.yml";

    /// <summary>
    /// Loads the options file using the specified <paramref name="directory"/> and <paramref name="file"/>.
    /// </summary>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns>An instance of <see cref="Generator"/> if the file exists; otherwise <c>null</c>.</returns>
    public GeneratorModel Load(string directory = null, string file = OptionsFileName)
    {
        var path = GetPath(directory, file);
        if (!File.Exists(path))
        {
            _logger.LogWarning($"Option file not found: {file}");
            return null;
        }

        _logger.LogInformation($"Loading options file: {file}");
        using var reader = File.OpenText(path);

        return Load(reader);
    }

    /// <summary>
    /// Loads the options using the specified <paramref name="reader" />
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>
    /// An instance of <see cref="Generator" />.
    /// </returns>
    public GeneratorModel Load(TextReader reader)
    {
        if (reader == null)
            return null;

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        // use Serialization model for better yaml support
        return deserializer.Deserialize<GeneratorModel>(reader);
    }

    /// <summary>
    /// Saves the generator options to the specified <paramref name="directory"/> and <paramref name="file"/>.
    /// </summary>
    /// <param name="generatorOptions">The generator options to save.</param>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns>The full path of the options file.</returns>
    public string Save(GeneratorModel generatorOptions, string directory = null, string file = OptionsFileName)
    {
        if (string.IsNullOrWhiteSpace(directory))
            directory = Environment.CurrentDirectory;

        if (string.IsNullOrWhiteSpace(file))
            file = OptionsFileName;

        if (!Directory.Exists(directory))
        {
            _logger.LogTrace($"Creating Directory: {directory}");
            Directory.CreateDirectory(directory);
        }

        _logger.LogInformation($"Saving options file: {file}");

        var path = Path.Combine(directory, file);

        var serializer = new SerializerBuilder()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults)
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        using (var streamWriter = File.CreateText(path))
            serializer.Serialize(streamWriter, generatorOptions);

        return path;
    }

    /// <summary>
    /// Determines if the specified options file exists.
    /// </summary>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns><c>true</c> if options file exits; otherwise <c>false</c>.</returns>
    public bool Exists(string directory = null, string file = OptionsFileName)
    {
        var path = GetPath(directory, file);
        return File.Exists(path);
    }


    private static string GetPath(string directory, string file)
    {
        if (string.IsNullOrWhiteSpace(directory))
            directory = Environment.CurrentDirectory;

        if (string.IsNullOrWhiteSpace(file))
            file = OptionsFileName;

        var path = Path.Combine(directory, file);
        return path;
    }
}
