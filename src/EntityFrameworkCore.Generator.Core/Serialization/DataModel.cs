namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Data class options
/// </summary>
public class DataModel
{
    /// <summary>
    /// Gets or sets the data context generation options.
    /// </summary>
    /// <value>
    /// The data context options
    /// </value>
    public ContextClass Context { get; set; } = new();


    /// <summary>
    /// Gets or sets the entity class generation options.
    /// </summary>
    /// <value>
    /// The entity class generation options.
    /// </value>
    public EntityClass Entity { get; set; } = new();

    /// <summary>
    /// Gets or sets the mapping class generation options.
    /// </summary>
    /// <value>
    /// The mapping class generation options.
    /// </value>
    public MappingClass Mapping { get; set; } = new();

    /// <summary>
    /// Gets or sets the query extension options.
    /// </summary>
    /// <value>
    /// The query extension options.
    /// </value>
    public QueryExtension Query { get; set; } = new();

}
