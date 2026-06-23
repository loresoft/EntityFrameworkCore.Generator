using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.AuditUpdateModel" /> update model for the <c>Audit</c> entity mapped to the <c>dbo.Audit</c> table.
/// </summary>
[RegisterSingleton<IValidator<AuditUpdateModel>>]
public partial class AuditUpdateModelValidator
    : AbstractValidator<AuditUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.AuditUpdateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.AuditUpdateModel" />.
    /// </summary>
    public AuditUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Content).NotEmpty();
        RuleFor(p => p.Username).NotEmpty();
        RuleFor(p => p.Username).MaximumLength(50);
        #endregion
    }

}
