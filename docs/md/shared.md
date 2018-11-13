# Shared Model Template 

The [Read](read.md), [Create](create.md) and [Update](update.md) model templates share the following [configuration](../configuration.md).

## Configuration

Shared configuration values are applied to all the model templates.

### namespace

The namespace for the model class. *Variables Supported*

### directory

The directory location to write the source file. *Variables Supported*

### exclude

The exclude configuration is a list regular expressions for entities and properties to exclude in the model.

#### entities

Exclude all entities that match any of the listed regular expressions.  

#### properties

Exclude all properties that match any of the listed regular expressions.  The value to match contains the parent entity the property belongs too, `Entity.Property`.

Example configuration

```YAML
model:
  shared:
    exclude:
      entities:
        - 'EmailDelivery'
        - 'UserLogin'
      properties:
        - 'User\.PasswordHash$'
        - 'User\.ResetHash$'
```