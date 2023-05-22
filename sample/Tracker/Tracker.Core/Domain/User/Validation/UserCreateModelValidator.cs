using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Validator class for <see cref="UserCreateModel"/> .
/// </summary>
public partial class UserCreateModelValidator
    : AbstractValidator<UserCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserCreateModelValidator"/> class.
    /// </summary>
    public UserCreateModelValidator()
    {
        #region Generated Constructor
            RuleFor(p => p.EmailAddress).NotEmpty();
            RuleFor(p => p.EmailAddress).MaximumLength(256);
            RuleFor(p => p.DisplayName).NotEmpty();
            RuleFor(p => p.DisplayName).MaximumLength(256);
            #endregion
    }

}
