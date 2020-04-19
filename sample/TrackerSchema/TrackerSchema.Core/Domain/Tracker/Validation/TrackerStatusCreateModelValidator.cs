using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Validation
{
    /// <summary>
    /// Validator class for <see cref="TrackerStatusCreateModel"/> .
    /// </summary>
    public partial class TrackerStatusCreateModelValidator
        : AbstractValidator<TrackerStatusCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerStatusCreateModelValidator"/> class.
        /// </summary>
        public TrackerStatusCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
