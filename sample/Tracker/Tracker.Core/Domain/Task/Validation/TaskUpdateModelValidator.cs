using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.TaskUpdateModel" /> update model for the <c>Task</c> entity mapped to the <c>dbo.Task</c> table.
/// </summary>
[RegisterSingleton<IValidator<TaskUpdateModel>>]
public partial class TaskUpdateModelValidator
    : AbstractValidator<TaskUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.TaskUpdateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.TaskUpdateModel" />.
    /// </summary>
    public TaskUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Title).NotEmpty();
        RuleFor(p => p.Title).MaximumLength(255);
        #endregion
    }

}
