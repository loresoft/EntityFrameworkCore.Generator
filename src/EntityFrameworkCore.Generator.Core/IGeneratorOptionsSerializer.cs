using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator;

/// <summary>
/// <c>interface</c> for serialization and deserialization of <see cref="GeneratorOptions"/> class
/// </summary>
public interface IGeneratorOptionsSerializer
{
    /// <summary>
    /// Loads the options file using the specified <paramref name="directory"/> and <paramref name="file"/>.
    /// </summary>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns>An instance of <see cref="GeneratorOptions"/> if the file exists; otherwise <c>null</c>.</returns>
    GeneratorOptions Load(string directory = null, string file = GeneratorOptionsSerializer.OptionsFileName);

    /// <summary>
    /// Saves the generator options to the specified <paramref name="directory"/> and <paramref name="file"/>.
    /// </summary>
    /// <param name="generatorOptions">The generator options to save.</param>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns>The full path of the options file.</returns>
    string Save(GeneratorOptions generatorOptions, string directory = null, string file = GeneratorOptionsSerializer.OptionsFileName);

    /// <summary>
    /// Determines if the specified options file exists.
    /// </summary>
    /// <param name="directory">The directory where the file is located.</param>
    /// <param name="file">The name of the options file.</param>
    /// <returns><c>true</c> if options file exits; otherwise <c>false</c>.</returns>
    bool Exists(string directory = null, string file = GeneratorOptionsSerializer.OptionsFileName);
}