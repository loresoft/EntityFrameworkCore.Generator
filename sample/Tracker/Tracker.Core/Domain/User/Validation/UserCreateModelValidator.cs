using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.UserCreateModel" /> create model for the <c>User</c> entity mapped to the <c>dbo.User</c> table.
/// </summary>
[RegisterSingleton<IValidator<UserCreateModel>>]
public partial class UserCreateModelValidator
    : AbstractValidator<UserCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.UserCreateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.UserCreateModel" />.
    /// </summary>
    public UserCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.EmailAddress).NotEmpty();
        RuleFor(p => p.EmailAddress).MaximumLength(256);
        RuleFor(p => p.DisplayName).NotEmpty();
        RuleFor(p => p.DisplayName).MaximumLength(256);
        #endregion
    }

}
