using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="TaskExtendedCreateModel"/> .
    /// </summary>
    public partial class TaskExtendedCreateModelValidator
        : AbstractValidator<TaskExtendedCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskExtendedCreateModelValidator"/> class.
        /// </summary>
        public TaskExtendedCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Browser).MaximumLength(256);
            RuleFor(p => p.OperatingSystem).MaximumLength(256);
            #endregion
        }

    }
}
