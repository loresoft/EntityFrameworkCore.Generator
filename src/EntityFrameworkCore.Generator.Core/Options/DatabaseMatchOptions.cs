using EntityFrameworkCore.Generator.Serialization;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Represents options for matching database tables and columns during a database operation.
/// </summary>
public class DatabaseMatchOptions : OptionsBase
{
    /// <summary>
    /// Represents options for matching database tables and columns during a database operation.
    /// </summary>
    /// <param name="variables">A dictionary of variables used to configure the matching options. Cannot be null.</param>
    /// <param name="prefix">An optional prefix used to filter or qualify the matching criteria. Can be null.</param>
    public DatabaseMatchOptions(VariableDictionary variables, string? prefix)
        : base(variables, prefix)
    {
        Tables = [];
        Columns = [];
    }

    /// <summary>
    /// Gets or sets a list of regular expression of tables to ignore.
    /// </summary>
    /// <value>
    /// The list of regular expression of tables to ignore.
    /// </value>
    public List<MatchOptions> Tables { get; set; }

    /// <summary>
    /// Gets or sets a list of regular expression of columns to ignore.
    /// </summary>
    /// <value>
    /// The list of regular expression of columns to ignore.
    /// </value>
    public List<MatchOptions> Columns { get; set; }
}
