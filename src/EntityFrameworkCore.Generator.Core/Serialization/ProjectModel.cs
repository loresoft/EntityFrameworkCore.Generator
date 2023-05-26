using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Project options
/// </summary>
public class ProjectModel
{
    /// <summary>
    /// Gets or sets the project root namespace.
    /// </summary>
    /// <value>
    /// The project root namespace.
    /// </value>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets or sets the project directory.
    /// </summary>
    /// <value>
    /// The project directory.
    /// </value>
    public string Directory { get; set; }


    /// <summary>
    /// Gets or sets if the output should support nullable reference types.
    /// </summary>
    /// <value>
    /// If the output should support nullable reference types.
    /// </value>
    [DefaultValue(false)]
    public bool Nullable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use file-scoped namespace.
    /// </summary>
    /// <value>
    ///   <c>true</c> to use file-coped namespace; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    public bool FileScopedNamespace { get; set; }
}
