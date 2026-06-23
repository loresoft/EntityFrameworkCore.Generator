using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.RoleUpdateModel" /> update model for the <c>Role</c> entity mapped to the <c>dbo.Role</c> table.
/// </summary>
[RegisterSingleton<IValidator<RoleUpdateModel>>]
public partial class RoleUpdateModelValidator
    : AbstractValidator<RoleUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.RoleUpdateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.RoleUpdateModel" />.
    /// </summary>
    public RoleUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(256);
        #endregion
    }

}
