namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Validator class options
/// </summary>
/// <seealso cref="ClassOptionsBase" />
public class ValidatorClassOptions : ClassOptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorClassOptions"/> class.
    /// </summary>
    public ValidatorClassOptions(VariableDictionary variables, string prefix)
        : base(variables, AppendPrefix(prefix, "Validator"))
    {
        Generate = false;
        Namespace = "{Project.Namespace}.Domain.Validation";
        Directory = @"{Project.Directory}\Domain\Validation";

        BaseClass = "AbstractValidator<{Model.Name}>";
        Name = "{Model.Name}Validator";
    }
    /// <summary>
    /// Gets or sets a value indicating whether this option is generated.
    /// </summary>
    /// <value>
    ///   <c>true</c> to generate; otherwise, <c>false</c>.
    /// </value>
    public bool Generate { get; set; }
}
