using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="RoleUpdateModel"/> .
    /// </summary>
    public partial class RoleUpdateModelValidator
        : AbstractValidator<RoleUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleUpdateModelValidator"/> class.
        /// </summary>
        public RoleUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(256);
            #endregion
        }

    }
}
