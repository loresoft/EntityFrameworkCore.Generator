namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Query extensions options
/// </summary>
public class QueryExtension : ClassBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryExtension"/> class.
    /// </summary>
    public QueryExtension()
    {
        Namespace = "{Project.Namespace}.Data.Queries";
        Directory = @"{Project.Directory}\Data\Queries";
        Name = "{Entity.Name}Extensions";

        Generate = false;
        IndexPrefix = "By";
        UniquePrefix = "GetBy";
    }

    /// <summary>
    /// Gets or sets a value indicating whether this option is generated.
    /// </summary>
    /// <value>
    ///   <c>true</c> to generate; otherwise, <c>false</c>.
    /// </value>
    public bool Generate { get; set; }

    /// <summary>
    /// Gets or sets the prefix of query method names.
    /// </summary>
    /// <value>
    /// The prefix of query method names
    /// </value>
    public string IndexPrefix { get; set; }

    /// <summary>
    /// Gets or sets the prefix of unique query method names.
    /// </summary>
    /// <value>
    /// The prefix of unique query method names.
    /// </value>
    public string UniquePrefix { get; set; }
}
