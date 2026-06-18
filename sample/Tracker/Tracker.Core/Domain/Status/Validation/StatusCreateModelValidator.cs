using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.StatusCreateModel" /> create model for the <c>Status</c> entity mapped to the <c>dbo.Status</c> table.
/// </summary>
[RegisterSingleton<IValidator<StatusCreateModel>>]
public partial class StatusCreateModelValidator
    : AbstractValidator<StatusCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.StatusCreateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.StatusCreateModel" />.
    /// </summary>
    public StatusCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(100);
        RuleFor(p => p.Description).MaximumLength(255);
        #endregion
    }

}
