using System;
using FluentValidation;
using Tracker.Domain.Models;

namespace Tracker.Domain.Validation
{
    public partial class SchemaversionsCreateModelValidator
        : AbstractValidator<SchemaversionsCreateModel>
    {
        public SchemaversionsCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Scriptname).NotEmpty();
            RuleFor(p => p.Scriptname).MaximumLength(255);
            #endregion
        }

    }
}
