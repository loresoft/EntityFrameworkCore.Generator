using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Validation
{
    /// <summary>
    /// Validator class for <see cref="RoleCreateModel"/> .
    /// </summary>
    public partial class RoleCreateModelValidator
        : AbstractValidator<RoleCreateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleCreateModelValidator"/> class.
        /// </summary>
        public RoleCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(256);
            #endregion
        }

    }
}
