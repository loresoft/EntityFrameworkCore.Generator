using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Sript template options
/// </summary>
public class TemplateModel
{
    /// <summary>
    /// Gets or sets the template file path.
    /// </summary>
    /// <value>
    /// The template file path.
    /// </value>
    public string TemplatePath { get; set; }

    /// <summary>
    /// Gets or sets the name of the class
    /// </summary>
    /// <value>
    /// The name of the class.
    /// </value>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets the class namespace.
    /// </summary>
    /// <value>
    /// The class namespace.
    /// </value>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets or sets the base class.
    /// </summary>
    /// <value>
    /// The base class.
    /// </value>
    public string BaseClass { get; set; }


    /// <summary>
    /// Gets or sets the output directory.  Default is the current working directory.
    /// </summary>
    /// <value>
    /// The output directory.
    /// </value>
    public string Directory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the generated file will be over written.
    /// </summary>
    /// <value>
    ///   <c>true</c> to overwrite generated file; otherwise, <c>false</c>.
    /// </value>
    public bool Overwrite { get; set; }

    /// <summary>
    /// Gets or sets the template parameters.
    /// </summary>
    /// <value>
    /// The template parameters.
    /// </value>
    public Dictionary<string, string> Parameters { get; set; }

}
