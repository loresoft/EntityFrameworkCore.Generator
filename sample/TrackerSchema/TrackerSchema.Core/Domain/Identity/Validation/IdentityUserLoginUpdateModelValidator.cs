using System;
using FluentValidation;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Validation
{
    /// <summary>
    /// Validator class for <see cref="IdentityUserLoginUpdateModel"/> .
    /// </summary>
    public partial class IdentityUserLoginUpdateModelValidator
        : AbstractValidator<IdentityUserLoginUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserLoginUpdateModelValidator"/> class.
        /// </summary>
        public IdentityUserLoginUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.EmailAddress).NotEmpty();
            RuleFor(p => p.EmailAddress).MaximumLength(256);
            RuleFor(p => p.Browser).MaximumLength(256);
            RuleFor(p => p.OperatingSystem).MaximumLength(256);
            RuleFor(p => p.DeviceFamily).MaximumLength(256);
            RuleFor(p => p.DeviceBrand).MaximumLength(256);
            RuleFor(p => p.DeviceModel).MaximumLength(256);
            RuleFor(p => p.IpAddress).MaximumLength(50);
            RuleFor(p => p.FailureMessage).MaximumLength(256);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
