using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Validation
{
    /// <summary>
    /// Validator class for <see cref="TrackerStatusUpdateModel"/> .
    /// </summary>
    public partial class TrackerStatusUpdateModelValidator
        : AbstractValidator<TrackerStatusUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerStatusUpdateModelValidator"/> class.
        /// </summary>
        public TrackerStatusUpdateModelValidator()
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
