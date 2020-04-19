using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Validation
{
    /// <summary>
    /// Validator class for <see cref="TrackerTaskCreateModel"/> .
    /// </summary>
    public partial class TrackerTaskCreateModelValidator
        : AbstractValidator<TrackerTaskCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerTaskCreateModelValidator"/> class.
        /// </summary>
        public TrackerTaskCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Title).MaximumLength(255);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
