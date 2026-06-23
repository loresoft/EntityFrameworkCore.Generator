using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.AuditCreateModel" /> create model for the <c>Audit</c> entity mapped to the <c>dbo.Audit</c> table.
/// </summary>
[RegisterSingleton<IValidator<AuditCreateModel>>]
public partial class AuditCreateModelValidator
    : AbstractValidator<AuditCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.AuditCreateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.AuditCreateModel" />.
    /// </summary>
    public AuditCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Content).NotEmpty();
        RuleFor(p => p.Username).NotEmpty();
        RuleFor(p => p.Username).MaximumLength(50);
        #endregion
    }

}
