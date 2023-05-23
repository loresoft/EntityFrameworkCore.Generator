using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Validator class for <see cref="UserRoleUpdateModel"/> .
/// </summary>
public partial class UserRoleUpdateModelValidator
    : AbstractValidator<UserRoleUpdateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleUpdateModelValidator"/> class.
    /// </summary>
    public UserRoleUpdateModelValidator()
    {
        #region Generated Constructor
        #endregion
    }

}
