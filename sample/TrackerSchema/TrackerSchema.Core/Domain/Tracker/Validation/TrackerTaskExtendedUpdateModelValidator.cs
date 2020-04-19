using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Validation
{
    /// <summary>
    /// Validator class for <see cref="TrackerTaskExtendedUpdateModel"/> .
    /// </summary>
    public partial class TrackerTaskExtendedUpdateModelValidator
        : AbstractValidator<TrackerTaskExtendedUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerTaskExtendedUpdateModelValidator"/> class.
        /// </summary>
        public TrackerTaskExtendedUpdateModelValidator()
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
