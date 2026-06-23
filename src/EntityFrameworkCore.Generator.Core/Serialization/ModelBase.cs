namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Base class for the Model generation
/// </summary>
public abstract class ModelBase : ClassBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelBase"/> class.
    /// </summary>
    protected ModelBase()
    {
        // null so shared option is used
        Namespace = null;
        Directory = null;

        Generate = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this option is generated.
    /// </summary>
    /// <value>
    ///   <c>true</c> to generate; otherwise, <c>false</c>.
    /// </value>
    public bool Generate { get; set; }

    /// <summary>
    /// Gets or sets the include selection options.
    /// </summary>
    /// <value>
    /// The include selection options.
    /// </value>
    public SelectionModel? Include { get; set; }

    /// <summary>
    /// Gets or sets the exclude selection options.
    /// </summary>
    /// <value>
    /// The exclude selection options.
    /// </value>
    public SelectionModel? Exclude { get; set; }
}
