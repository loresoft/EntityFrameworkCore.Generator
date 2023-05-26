using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Script options
/// </summary>
public class ScriptModel
{
    /// <summary>
    /// Gets or sets the list context script templates.
    /// </summary>
    /// <value>The list context script templates.</value>
    public List<TemplateModel> Context { get; set; }

    /// <summary>
    /// Gets or sets the list entity script templates.
    /// </summary>
    /// <value>The list entity script templates.</value>
    public List<TemplateModel> Entity { get; set; }

    /// <summary>
    /// Gets or sets the list model script templates.
    /// </summary>
    /// <value>The list model script templates.</value>
    public List<TemplateModel> Model { get; set; }
}
