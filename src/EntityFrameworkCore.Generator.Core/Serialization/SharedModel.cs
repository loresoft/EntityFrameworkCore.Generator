using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Shared model options
/// </summary>
public class SharedModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SharedModelOptions"/> class.
    /// </summary>
    public SharedModel()
    {
        Namespace = "{Project.Namespace}.Domain.Models";
        Directory = @"{Project.Directory}\Domain\Models";
    }

    /// <summary>
    /// Gets or sets the class namespace.
    /// </summary>
    /// <value>
    /// The class namespace.
    /// </value>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets or sets the output directory.
    /// </summary>
    /// <value>
    /// The output directory.
    /// </value>
    public string Directory { get; set; }

    /// <summary>
    /// Gets or sets the file header.
    /// </summary>
    public string? Header { get; set; }

    /// <summary>
    /// Gets or sets the include selection options.
    /// </summary>
    /// <value>
    /// The include selection options.
    /// </value>
    public SelectionModel? Include { get; set; }

    /// <summary>
    /// Gets or sets the exclude selection options.
    /// </summary>
    /// <value>
    /// The exclude selection options.
    /// </value>
    public SelectionModel? Exclude { get; set; }
}
