using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Selection options
/// </summary>
public class SelectionOptions : OptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionOptions"/> class.
    /// </summary>
    public SelectionOptions(VariableDictionary variables, string? prefix)
        : base(variables, prefix)
    {
        Entities = [];
        Properties = [];
    }

    /// <summary>
    /// Gets or sets a list of regular expression of entities to select.
    /// </summary>
    /// <value>
    /// The list of regular expression of entities to select.
    /// </value>
    public List<MatchOptions> Entities { get; }

    /// <summary>
    /// Gets or sets a list of regular expression of properties to select.
    /// </summary>
    /// <value>
    /// The list of regular expression of properties to select.
    /// </value>
    public List<MatchOptions> Properties { get; }
}
