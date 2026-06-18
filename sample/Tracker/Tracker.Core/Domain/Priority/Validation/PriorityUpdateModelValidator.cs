using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.PriorityUpdateModel" /> update model for the <c>Priority</c> entity mapped to the <c>dbo.Priority</c> table.
/// </summary>
[RegisterSingleton<IValidator<PriorityUpdateModel>>]
public partial class PriorityUpdateModelValidator
    : AbstractValidator<PriorityUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.PriorityUpdateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.PriorityUpdateModel" />.
    /// </summary>
    public PriorityUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(100);
        RuleFor(p => p.Description).MaximumLength(255);
        #endregion
    }

}
