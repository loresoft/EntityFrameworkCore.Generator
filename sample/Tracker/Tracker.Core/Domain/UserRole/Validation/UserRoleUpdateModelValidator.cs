using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.UserRoleUpdateModel" /> update model for the <c>UserRole</c> entity mapped to the <c>dbo.UserRole</c> table.
/// </summary>
[RegisterSingleton<IValidator<UserRoleUpdateModel>>]
public partial class UserRoleUpdateModelValidator
    : AbstractValidator<UserRoleUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.UserRoleUpdateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.UserRoleUpdateModel" />.
    /// </summary>
    public UserRoleUpdateModelValidator()
    {
        #region Generated Constructor
        #endregion
    }

}
