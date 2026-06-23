using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.TenantUpdateModel" /> update model for the <c>Tenant</c> entity mapped to the <c>dbo.Tenant</c> table.
/// </summary>
[RegisterSingleton<IValidator<TenantUpdateModel>>]
public partial class TenantUpdateModelValidator
    : AbstractValidator<TenantUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.TenantUpdateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.TenantUpdateModel" />.
    /// </summary>
    public TenantUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(256);
        #endregion
    }

}
