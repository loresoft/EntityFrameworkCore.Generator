using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="TenantCreateModel"/> .
    /// </summary>
    public partial class TenantCreateModelValidator
        : AbstractValidator<TenantCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantCreateModelValidator"/> class.
        /// </summary>
        public TenantCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
            #endregion
        }

    }
}
