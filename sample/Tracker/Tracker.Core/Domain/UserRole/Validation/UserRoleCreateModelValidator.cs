using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.UserRoleCreateModel" /> create model for the <c>UserRole</c> entity mapped to the <c>dbo.UserRole</c> table.
/// </summary>
[RegisterSingleton<IValidator<UserRoleCreateModel>>]
public partial class UserRoleCreateModelValidator
    : AbstractValidator<UserRoleCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.UserRoleCreateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.UserRoleCreateModel" />.
    /// </summary>
    public UserRoleCreateModelValidator()
    {
        #region Generated Constructor
        #endregion
    }

}
