using System;
using FluentValidation;
using Tracker.Domain.Models;

namespace Tracker.Domain.Validation
{
    public partial class UserRoleUpdateModelValidator
        : AbstractValidator<UserRoleUpdateModel>
    {
        public UserRoleUpdateModelValidator()
        {
            #region Generated Constructor
            #endregion
        }

    }
}
