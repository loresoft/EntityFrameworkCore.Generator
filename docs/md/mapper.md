# Model Object Mapper Template

Create AutoMapper profiles to map entity to models.  Requires nuget package `AutoMapper`.

## Output

Example of a mapper profile class

```C#
public partial class StatusProfile
    : Profile
{
    public StatusProfile()
    {
        CreateMap<Status, StatusReadModel>();
        CreateMap<StatusCreateModel, Status>();
        CreateMap<StatusUpdateModel, Status>();
        CreateCustomMap();
    }

    partial void CreateCustomMap();
}
```

## Configuration

The mapper template has the following configuration that can be set in the yaml [configuration file](../configuration.md).

Example configuration

```YAML
model:
  mapper:
    generate: true
    name: '{Entity.Name}Profile'
    baseClass: Profile
    namespace: '{Project.Namespace}.Domain.Mapping'
    directory: '{Project.Directory}\Domain\Mapping'
```

### generate

Flag to enable generating the output for this template.  Default: `true`

### name

The mapper class name. Default: `{Entity.Name}Profile`  *Variables Supported*

### baseClass

The base class to inherit from. Default: `Profile`  *Variables Supported*

### namespace

The namespace for the mapper class. *Variables Supported*

### directory

The directory location to write the source file. *Variables Supported*

### document

Include XML documentation for the generated mapper class.  Default: `false`

## Regeneration

The mapper template does not support regeneration.  The mapper class will only be created if one doesn't exist for an entity.