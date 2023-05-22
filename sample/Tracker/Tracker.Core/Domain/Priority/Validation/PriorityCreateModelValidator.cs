using System;
using FluentValidation;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Validation;

/// <summary>
/// Validator class for <see cref="PriorityCreateModel"/> .
/// </summary>
public partial class PriorityCreateModelValidator
    : AbstractValidator<PriorityCreateModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityCreateModelValidator"/> class.
    /// </summary>
    public PriorityCreateModelValidator()
    {
        #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(255);
            #endregion
    }

}
