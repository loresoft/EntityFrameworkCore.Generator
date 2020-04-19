using System;
using FluentValidation;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Validation
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
