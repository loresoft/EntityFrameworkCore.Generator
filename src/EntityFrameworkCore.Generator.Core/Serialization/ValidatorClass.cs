using System.ComponentModel;

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

        BaseClass = "FluentValidation.AbstractValidator<{Model.Name}>";
        Name = "{Model.Name}Validator";
    }
    /// <summary>
    /// Gets or sets a value indicating whether this option is generated.
    /// </summary>
    /// <value>
    ///   <c>true</c> to generate; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    public bool Generate { get; set; }
}
