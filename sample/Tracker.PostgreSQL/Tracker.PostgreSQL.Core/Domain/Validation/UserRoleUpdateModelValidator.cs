using System;
using FluentValidation;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Validation
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
