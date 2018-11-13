# Create Model Template

The create model template generates a create model from an entity.  The create model is used to shape creating new entity instances.

## Output

Example of a generated model class

```c#
public partial class StatusCreateModel
    : EntityCreateModel
{
    #region Generated Properties
    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }
    #endregion
}
```

## Configuration

The create model template has the following configuration that can be set in the yaml [configuration file](../configuration.md).

Configuration values set at the this level override the shared model configuration.

Example configuration

```YAML
model:
  create:
    generate: true
    name: '{Entity.Name}CreateModel'
    baseClass: EntityCreateModel
    namespace: '{Project.Namespace}.Domain.Models'
    directory: '{Project.Directory}\Domain\Models'
    exclude:
      entities:
        - 'EmailDelivery'
        - 'UserLogin'
      properties:
        - 'User\.PasswordHash$'
        - 'User\.ResetHash$'
```

### generate

Flag to enable generating the output for this template.  Default: `true`

### name

The model class name. Default: `{Entity.Name}CreateModel`  *Variables Supported*

### baseClass

The base class to inherit from.  *Variables Supported*

### namespace

The namespace for the model class. *Variables Supported*

### directory

The directory location to write the source file. *Variables Supported*

### document

Include XML documentation for the generated model class.  Default: `false`

### exclude

A list regular expressions for entities and properties to exclude in the model.

#### entities

Exclude all entities that match any of the listed regular expressions.  

#### properties

Exclude all properties that match any of the listed regular expressions.  The value to match contains the parent entity the property belongs too, `Entity.Property`.

## Regeneration

The model template has one region that is replaced on regeneration.

### Generated Properties

The `Generated Properties` region contains all the properties for the model that are mapped to an entity.
