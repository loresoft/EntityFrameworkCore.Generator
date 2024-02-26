using System;
using FluentValidation;

using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Validator class for <see cref="AuditCreateModel"/> .
/// </summary>
[RegisterSingleton<IValidator<AuditCreateModel>>]
public partial class AuditCreateModelValidator
    : AbstractValidator<AuditCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuditCreateModelValidator"/> class.
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
