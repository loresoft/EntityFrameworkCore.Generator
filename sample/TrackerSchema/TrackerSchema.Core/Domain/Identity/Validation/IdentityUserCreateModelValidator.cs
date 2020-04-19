using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Validation
{
    /// <summary>
    /// Validator class for <see cref="IdentityUserCreateModel"/> .
    /// </summary>
    public partial class IdentityUserCreateModelValidator
        : AbstractValidator<IdentityUserCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserCreateModelValidator"/> class.
        /// </summary>
        public IdentityUserCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.EmailAddress).NotEmpty();
            RuleFor(p => p.EmailAddress).MaximumLength(256);
            RuleFor(p => p.DisplayName).NotEmpty();
            RuleFor(p => p.DisplayName).MaximumLength(256);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
