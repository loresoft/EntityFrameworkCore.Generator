using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.PriorityCreateModel" /> create model for the <c>Priority</c> entity mapped to the <c>dbo.Priority</c> table.
/// </summary>
[RegisterSingleton<IValidator<PriorityCreateModel>>]
public partial class PriorityCreateModelValidator
    : AbstractValidator<PriorityCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.PriorityCreateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.PriorityCreateModel" />.
    /// </summary>
    public PriorityCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(100);
        RuleFor(p => p.Description).MaximumLength(255);
        #endregion
    }

}
