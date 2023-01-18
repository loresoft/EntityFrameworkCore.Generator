namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Create model file generation options
/// </summary>
/// <seealso cref="EntityFrameworkCore.Generator.Options.ModelOptionsBase" />
public class CreateModelOptions : ModelOptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateModelOptions"/> class.
    /// </summary>
    public CreateModelOptions(VariableDictionary variables, string prefix)
        : base(variables, AppendPrefix(prefix, "Create"))
    {
        Name = "{Entity.Name}CreateModel";
    }
}