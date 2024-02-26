using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Validator class for <see cref="AuditUpdateModel"/> .
/// </summary>
[RegisterSingleton<IValidator<AuditUpdateModel>>]
public partial class AuditUpdateModelValidator
    : AbstractValidator<AuditUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuditUpdateModelValidator"/> class.
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
