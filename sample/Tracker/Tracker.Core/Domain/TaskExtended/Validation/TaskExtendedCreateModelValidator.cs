using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.TaskExtendedCreateModel" /> create model for the <c>TaskExtended</c> entity mapped to the <c>dbo.TaskExtended</c> table.
/// </summary>
[RegisterSingleton<IValidator<TaskExtendedCreateModel>>]
public partial class TaskExtendedCreateModelValidator
    : AbstractValidator<TaskExtendedCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.TaskExtendedCreateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.TaskExtendedCreateModel" />.
    /// </summary>
    public TaskExtendedCreateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Browser).MaximumLength(256);
        RuleFor(p => p.OperatingSystem).MaximumLength(256);
        #endregion
    }

}
