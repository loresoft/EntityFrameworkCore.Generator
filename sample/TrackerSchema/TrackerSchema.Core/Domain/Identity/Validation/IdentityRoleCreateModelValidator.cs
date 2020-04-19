using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Validation
{
    /// <summary>
    /// Validator class for <see cref="IdentityRoleCreateModel"/> .
    /// </summary>
    public partial class IdentityRoleCreateModelValidator
        : AbstractValidator<IdentityRoleCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityRoleCreateModelValidator"/> class.
        /// </summary>
        public IdentityRoleCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(256);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
