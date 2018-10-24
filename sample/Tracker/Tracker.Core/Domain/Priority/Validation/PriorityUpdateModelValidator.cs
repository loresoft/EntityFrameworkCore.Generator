using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="PriorityUpdateModel"/> .
    /// </summary>
    public partial class PriorityUpdateModelValidator
        : AbstractValidator<PriorityUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityUpdateModelValidator"/> class.
        /// </summary>
        public PriorityUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
            #endregion
        }

    }
}
