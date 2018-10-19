using System;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Validator class options
    /// </summary>
    /// <seealso cref="ClassOptionsBase" />
    public class ValidatorClassOptions : ClassOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorClassOptions"/> class.
        /// </summary>
        public ValidatorClassOptions()
        {
            Namespace = "{Project.Namespace}.Domain.Validation";
            Directory = @"{Project.Directory}\Domain\Validation";

            BaseClass = "AbstractValidator<{Model.Name}>";
            Name = "{Model.Name}Validator";
        }

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
}