using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Selection options
/// </summary>
public class SelectionModel
{
    /// <summary>
    /// Gets or sets a list of regular expression of entities to select.
    /// </summary>
    /// <value>
    /// The list of regular expression of entities to select.
    /// </value>
    public List<MatchModel>? Entities { get; set; }

    /// <summary>
    /// Gets or sets a list of regular expression of properties to select.
    /// </summary>
    /// <value>
    /// The list of regular expression of properties to select.
    /// </value>
    public List<MatchModel>? Properties { get; set; }

}
