using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="StatusCreateModel"/> .
    /// </summary>
    public partial class StatusCreateModelValidator
        : AbstractValidator<StatusCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCreateModelValidator"/> class.
        /// </summary>
        public StatusCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
            #endregion
        }

    }
}
