using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="TenantUpdateModel"/> .
    /// </summary>
    public partial class TenantUpdateModelValidator
        : AbstractValidator<TenantUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantUpdateModelValidator"/> class.
        /// </summary>
        public TenantUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(256);
            #endregion
        }

    }
}
