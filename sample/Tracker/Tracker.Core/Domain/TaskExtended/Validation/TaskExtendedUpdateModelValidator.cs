using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="TaskExtendedUpdateModel"/> .
    /// </summary>
    public partial class TaskExtendedUpdateModelValidator
        : AbstractValidator<TaskExtendedUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskExtendedUpdateModelValidator"/> class.
        /// </summary>
        public TaskExtendedUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Browser).MaximumLength(256);
            RuleFor(p => p.OperatingSystem).MaximumLength(256);
            #endregion
        }

    }
}
