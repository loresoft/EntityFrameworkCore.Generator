using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Validation
{
    /// <summary>
    /// Validator class for <see cref="TrackerTaskUpdateModel"/> .
    /// </summary>
    public partial class TrackerTaskUpdateModelValidator
        : AbstractValidator<TrackerTaskUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerTaskUpdateModelValidator"/> class.
        /// </summary>
        public TrackerTaskUpdateModelValidator()
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
