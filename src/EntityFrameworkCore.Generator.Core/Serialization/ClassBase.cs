using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Base class for Class generation
/// </summary>
public abstract class ClassBase
{
    /// <summary>
    /// Gets or sets the class namespace.
    /// </summary>
    /// <value>
    /// The class namespace.
    /// </value>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets or sets the output directory.  Default is the current working directory.
    /// </summary>
    /// <value>
    /// The output directory.
    /// </value>
    public string Directory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to create xml documentation.
    /// </summary>
    /// <value>
    ///   <c>true</c> to create xml documentation; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    public bool Document { get; set; }
}
