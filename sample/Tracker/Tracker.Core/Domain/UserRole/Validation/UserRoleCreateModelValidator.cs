using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="UserRoleCreateModel"/> .
    /// </summary>
    public partial class UserRoleCreateModelValidator
        : AbstractValidator<UserRoleCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleCreateModelValidator"/> class.
        /// </summary>
        public UserRoleCreateModelValidator()
        {
            #region Generated Constructor
            #endregion
        }

    }
}
