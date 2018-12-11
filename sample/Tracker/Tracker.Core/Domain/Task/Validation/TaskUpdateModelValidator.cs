using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="TaskUpdateModel"/> .
    /// </summary>
    public partial class TaskUpdateModelValidator
        : AbstractValidator<TaskUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskUpdateModelValidator"/> class.
        /// </summary>
        public TaskUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Title).MaximumLength(255);
            #endregion
        }

    }
}
