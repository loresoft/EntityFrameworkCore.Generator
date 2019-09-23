using System;
using FluentValidation;
using Tracker.Domain.Models;

namespace Tracker.Domain.Validation
{
    public partial class UserRoleCreateModelValidator
        : AbstractValidator<UserRoleCreateModel>
    {
        public UserRoleCreateModelValidator()
        {
            #region Generated Constructor
            #endregion
        }

    }
}
