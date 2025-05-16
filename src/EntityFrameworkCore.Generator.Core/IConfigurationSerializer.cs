using System.IO;

namespace EntityFrameworkCore.Generator;

/// <summary>
/// <c>interface</c> for serialization and deserialization of <see cref="Serialization.GeneratorModel"/> class
/// </summary>
public interface IConfigurationSerializer
{
    /// <summary>
    /// Loads the options file using the specified <paramref name="directory"/> and <paramref name="file"/>.
    /// </summary>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns>An instance of <see cref="Serialization.GeneratorModel"/> if the file exists; otherwise <c>null</c>.</returns>
    Serialization.GeneratorModel? Load(string? directory = null, string file = ConfigurationSerializer.OptionsFileName);

    /// <summary>
    /// Loads the options using the specified <paramref name="reader" />
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>
    /// An instance of <see cref="Generator" />.
    /// </returns>
    Serialization.GeneratorModel? Load(TextReader reader);

    /// <summary>
    /// Saves the generator options to the specified <paramref name="directory"/> and <paramref name="file"/>.
    /// </summary>
    /// <param name="generatorOptions">The generator options to save.</param>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns>The full path of the options file.</returns>
    string Save(Serialization.GeneratorModel generatorOptions, string? directory = null, string file = ConfigurationSerializer.OptionsFileName);

    /// <summary>
    /// Determines if the specified options file exists.
    /// </summary>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns><c>true</c> if options file exits; otherwise <c>false</c>.</returns>
    bool Exists(string? directory = null, string file = ConfigurationSerializer.OptionsFileName);
}
