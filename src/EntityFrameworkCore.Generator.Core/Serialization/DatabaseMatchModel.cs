namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Represents a model that specifies patterns for tables and columns to be ignored during processing.
/// </summary>
public class DatabaseMatchModel
{
    /// <summary>
    /// Gets or sets a list of regular expression of tables to ignore.
    /// </summary>
    /// <value>
    /// The list of regular expression of tables to ignore.
    /// </value>
    public List<MatchModel>? Tables { get; set; }

    /// <summary>
    /// Gets or sets a list of regular expression of columns to ignore.
    /// </summary>
    /// <value>
    /// The list of regular expression of columns to ignore.
    /// </value>
    public List<MatchModel>? Columns { get; set; }

    /// <summary>
    /// Gets or sets a list of regular expression of relationships to ignore.
    /// </summary>
    /// <value>
    /// The list of regular expression of relationships to ignore.
    /// </value>
    public List<MatchModel>? Relationships { get; set; }
}
