namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Validator class options
/// </summary>
public class ValidatorClass : ClassBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorClass"/> class.
    /// </summary>
    public ValidatorClass()
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

    /// <summary>
    /// Gets or sets the validator class name template.
    /// </summary>
    /// <value>
    /// The validator class name template.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the base class to inherit from.
    /// </summary>
    /// <value>
    /// The base class.
    /// </value>
    public string BaseClass { get; set; }
}
