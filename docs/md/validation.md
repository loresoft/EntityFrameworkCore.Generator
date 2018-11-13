# Model Validation Template

Create a FluentValidation class for the [Create](create.md) and [Update](update.md) models.  Requires nuget package `FluentValidation`.

## Output

Example of a validation class

```C#
public partial class StatusUpdateModelValidator
    : AbstractValidator<StatusUpdateModel>
{
    public StatusUpdateModelValidator()
    {
        #region Generated Constructor
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MaximumLength(100);
        RuleFor(p => p.Description).MaximumLength(255);
        #endregion
    }
}
```

## Configuration

The validation template has the following configuration that can be set in the yaml [configuration file](../configuration.md).

Example configuration

```YAML
model:
  validator:
    generate: true
    name: '{Model.Name}Validator'
    baseClass: AbstractValidator<{Model.Name}>
    namespace: '{Project.Namespace}.Domain.Validation'
    directory: '{Project.Directory}\Domain\Validation'
```

### generate

Flag to enable generating the output for this template.  Default: `true`

### name

The validation class name. Default: `{Model.Name}Validator`  *Variables Supported*

### baseClass

The base class to inherit from. Default: `AbstractValidator<{Model.Name}>`  *Variables Supported*

### namespace

The namespace for the validation class. *Variables Supported*

### directory

The directory location to write the source file. *Variables Supported*

### document

Include XML documentation for the generated validation class.  Default: `false`

## Regeneration

The validation template has one region that is replaced on regeneration.

### Generated Constructor

The `Generated Constructor` region initializes any model rules that can be derived from the database context.