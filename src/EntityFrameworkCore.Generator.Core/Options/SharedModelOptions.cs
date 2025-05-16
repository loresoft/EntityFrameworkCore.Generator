namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Shared model options
/// </summary>
/// <seealso cref="OptionsBase" />
public class SharedModelOptions : OptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SharedModelOptions"/> class.
    /// </summary>
    /// <param name="variables">The shared variables dictionary.</param>
    /// <param name="prefix">The variable key prefix.</param>
    public SharedModelOptions(VariableDictionary variables, string? prefix)
        : base(variables, prefix)
    {
        Namespace = "{Project.Namespace}.Domain.Models";
        Directory = @"{Project.Directory}\Domain\Models";

        Include = new SelectionOptions(variables, AppendPrefix(prefix, "Include"));
        Exclude = new SelectionOptions(variables, AppendPrefix(prefix, "Exclude"));
    }

    /// <summary>
    /// Gets or sets the class namespace.
    /// </summary>
    /// <value>
    /// The class namespace.
    /// </value>
    public string? Namespace
    {
        get => GetProperty();
        set => SetProperty(value);
    }

    /// <summary>
    /// Gets or sets the output directory.
    /// </summary>
    /// <value>
    /// The output directory.
    /// </value>
    public string? Directory
    {
        get => GetProperty();
        set => SetProperty(value);
    }

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
