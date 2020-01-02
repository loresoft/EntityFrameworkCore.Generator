using System;
using FluentValidation;
using Tracker.Domain.Models;

namespace Tracker.Domain.Validation
{
    public partial class RoleUpdateModelValidator
        : AbstractValidator<RoleUpdateModel>
    {
        public RoleUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(256);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
