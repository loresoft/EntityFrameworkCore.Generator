using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="TaskCreateModel"/> .
    /// </summary>
    public partial class TaskCreateModelValidator
        : AbstractValidator<TaskCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskCreateModelValidator"/> class.
        /// </summary>
        public TaskCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Title).MaximumLength(255);
            #endregion
        }

    }
}
