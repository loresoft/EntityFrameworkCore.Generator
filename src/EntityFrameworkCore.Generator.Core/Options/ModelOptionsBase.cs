namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Base class for the Model generation
/// </summary>
/// <seealso cref="ClassOptionsBase" />
public abstract class ModelOptionsBase : ClassOptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelOptionsBase"/> class.
    /// </summary>
    protected ModelOptionsBase(VariableDictionary variables, string? prefix)
        : base(variables, prefix)
    {
        // null so shared option is used
        Namespace = null;
        Directory = null;

        Generate = false;

        Include = new SelectionOptions(variables, AppendPrefix(prefix, "Include"));
        Exclude = new SelectionOptions(variables, AppendPrefix(prefix, "Exclude"));
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
    public SelectionOptions Include { get; }

    /// <summary>
    /// Gets or sets the exclude selection options.
    /// </summary>
    /// <value>
    /// The exclude selection options.
    /// </value>
    public SelectionOptions Exclude { get; }
}
