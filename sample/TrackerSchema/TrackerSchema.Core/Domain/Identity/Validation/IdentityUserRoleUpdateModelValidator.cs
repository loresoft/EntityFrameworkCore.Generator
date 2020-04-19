using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Validation
{
    /// <summary>
    /// Validator class for <see cref="IdentityUserRoleUpdateModel"/> .
    /// </summary>
    public partial class IdentityUserRoleUpdateModelValidator
        : AbstractValidator<IdentityUserRoleUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserRoleUpdateModelValidator"/> class.
        /// </summary>
        public IdentityUserRoleUpdateModelValidator()
        {
            #region Generated Constructor
            #endregion
        }

    }
}
