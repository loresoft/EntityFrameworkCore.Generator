using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Validation
{
    /// <summary>
    /// Validator class for <see cref="IdentityUserRoleCreateModel"/> .
    /// </summary>
    public partial class IdentityUserRoleCreateModelValidator
        : AbstractValidator<IdentityUserRoleCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserRoleCreateModelValidator"/> class.
        /// </summary>
        public IdentityUserRoleCreateModelValidator()
        {
            #region Generated Constructor
            #endregion
        }

    }
}
