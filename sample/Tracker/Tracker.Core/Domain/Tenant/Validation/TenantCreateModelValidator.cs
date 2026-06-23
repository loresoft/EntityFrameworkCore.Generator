using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.TenantCreateModel" /> create model for the <c>Tenant</c> entity mapped to the <c>dbo.Tenant</c> table.
/// </summary>
[RegisterSingleton<IValidator<TenantCreateModel>>]
public partial class TenantCreateModelValidator
    : AbstractValidator<TenantCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.TenantCreateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.TenantCreateModel" />.
    /// </summary>
    public TenantCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(256);
        #endregion
    }

}
