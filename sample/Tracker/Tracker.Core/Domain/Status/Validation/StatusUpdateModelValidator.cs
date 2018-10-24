using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="StatusUpdateModel"/> .
    /// </summary>
    public partial class StatusUpdateModelValidator
        : AbstractValidator<StatusUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusUpdateModelValidator"/> class.
        /// </summary>
        public StatusUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
            #endregion
        }

    }
}
