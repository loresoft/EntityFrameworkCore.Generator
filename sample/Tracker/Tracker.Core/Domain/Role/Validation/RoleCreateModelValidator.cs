using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.RoleCreateModel" /> create model for the <c>Role</c> entity mapped to the <c>dbo.Role</c> table.
/// </summary>
[RegisterSingleton<IValidator<RoleCreateModel>>]
public partial class RoleCreateModelValidator
    : AbstractValidator<RoleCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.RoleCreateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.RoleCreateModel" />.
    /// </summary>
    public RoleCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(256);
        #endregion
    }

}
