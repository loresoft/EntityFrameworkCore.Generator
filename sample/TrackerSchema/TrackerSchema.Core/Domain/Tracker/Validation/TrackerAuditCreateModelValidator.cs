using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Validation
{
    /// <summary>
    /// Validator class for <see cref="TrackerAuditCreateModel"/> .
    /// </summary>
    public partial class TrackerAuditCreateModelValidator
        : AbstractValidator<TrackerAuditCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerAuditCreateModelValidator"/> class.
        /// </summary>
        public TrackerAuditCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Content).NotEmpty();
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MaximumLength(50);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
