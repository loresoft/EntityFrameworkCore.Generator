namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Model options group
/// </summary>
public class ModelOptions : OptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelOptions" /> class.
    /// </summary>
    /// <param name="variables">The shared variables dictionary.</param>
    /// <param name="prefix">The variable key prefix.</param>
    public ModelOptions(VariableDictionary variables, string? prefix)
        : base(variables, AppendPrefix(prefix, "Model"))
    {
        Shared = new SharedModelOptions(Variables, Prefix);
        Read = new ReadModelOptions(Variables, Prefix);
        Create = new CreateModelOptions(Variables, Prefix);
        Update = new UpdateModelOptions(Variables, Prefix);
        Mapper = new MapperClassOptions(Variables, Prefix);
        Validator = new ValidatorClassOptions(Variables, Prefix);
    }

    /// <summary>
    /// Gets or sets the shared options between read,create and update models
    /// </summary>
    /// <value>
    /// The shared options between read,create and update models.
    /// </value>
    public SharedModelOptions Shared { get; }

    /// <summary>
    /// Gets or sets the read model options.
    /// </summary>
    /// <value>
    /// The read model options.
    /// </value>
    public ReadModelOptions Read { get; }

    /// <summary>
    /// Gets or sets the create model options.
    /// </summary>
    /// <value>
    /// The create model options.
    /// </value>
    public CreateModelOptions Create { get; }

    /// <summary>
    /// Gets or sets the update model options.
    /// </summary>
    /// <value>
    /// The update model options.
    /// </value>
    public UpdateModelOptions Update { get; }

    /// <summary>
    /// Gets or sets the view model mapper options.
    /// </summary>
    /// <value>
    /// The view model mapper options.
    /// </value>
    public MapperClassOptions Mapper { get; }

    /// <summary>
    /// Gets or sets the model validator options.
    /// </summary>
    /// <value>
    /// The model validator options.
    /// </value>
    public ValidatorClassOptions Validator { get; }

}
