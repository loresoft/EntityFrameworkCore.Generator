using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Validation
{
    /// <summary>
    /// Validator class for <see cref="TrackerTaskExtendedCreateModel"/> .
    /// </summary>
    public partial class TrackerTaskExtendedCreateModelValidator
        : AbstractValidator<TrackerTaskExtendedCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerTaskExtendedCreateModelValidator"/> class.
        /// </summary>
        public TrackerTaskExtendedCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Browser).MaximumLength(256);
            RuleFor(p => p.OperatingSystem).MaximumLength(256);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
