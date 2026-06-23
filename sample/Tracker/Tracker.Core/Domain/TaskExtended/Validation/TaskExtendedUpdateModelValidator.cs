using System;

using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Defines FluentValidation rules for the <see cref="Tracker.Core.Domain.Models.TaskExtendedUpdateModel" /> update model for the <c>TaskExtended</c> entity mapped to the <c>dbo.TaskExtended</c> table.
/// </summary>
[RegisterSingleton<IValidator<TaskExtendedUpdateModel>>]
public partial class TaskExtendedUpdateModelValidator
    : AbstractValidator<TaskExtendedUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Validation.TaskExtendedUpdateModelValidator"/> class and configures validation rules for <see cref="Tracker.Core.Domain.Models.TaskExtendedUpdateModel" />.
    /// </summary>
    public TaskExtendedUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Browser).MaximumLength(256);
        RuleFor(p => p.OperatingSystem).MaximumLength(256);
        #endregion
    }

}
