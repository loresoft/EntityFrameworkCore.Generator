# Configuration Variables

Entity Framework Core Generator supports context variables in the yaml configuration file.

## Variable syntax

To use the variable in the yaml file, wrap them with `{}` brackets.  Example `{Database.Name}`


## Common Variables

The following are common variables to use in the configuration file

### Database Name

`{Database.Name}` is the name of the database used for generation

### Project Namespace

`{Project.Namespace}` The root namespace for the generated project files

### Project Directory

`{Project.Directory}` The root directory for the files generated

### Table Schema

`{Table.Schema}` is the name of the current table schema

### Table Name

`{Table.Name}` is the name of the current table

### Entity Name

`{Entity.Name}` is the name of the current entity

### Model Name

`{Model.Name}` is the name of the current model

## Examples

Use the table schema to organize entities into folders

```YAML
data:
  context:
    name: '{Database.Name}Context'
    baseClass: DbContext
    propertyNaming: Plural
    namespace: '{Project.Namespace}.Data'
    directory: '{Project.Directory}\Data'
  entity:
    name: '{Table.Schema}{Table.Name}'
    namespace: '{Project.Namespace}.Data.{Table.Schema}.Entities'
    directory: '{Project.Directory}\Data\{Table.Schema}\Entities'
  mapping:
    namespace: '{Project.Namespace}.Data.{Table.Schema}.Mapping'
    directory: '{Project.Directory}\Data\{Table.Schema}\Mapping'
  query:
    generate: true
    indexPrefix: By
    uniquePrefix: GetBy
    namespace: '{Project.Namespace}.Data.{Table.Schema}.Queries'
    directory: '{Project.Directory}\Data\{Table.Schema}\Queries'
```

Use the entity name to sort domain models into folders

```YAML
model:
  shared:
    namespace: '{Project.Namespace}.Domain.Models'
    directory: '{Project.Directory}\Domain\{Entity.Name}\Models'
  read:
    generate: true
    name: '{Entity.Name}ReadModel'
    baseClass: 'EntityReadModel'
  create:
    generate: true
    name: '{Entity.Name}CreateModel'
    baseClass: 'EntityCreateModel'
  update:
    generate: true
    name: '{Entity.Name}UpdateModel'
    baseClass: EntityUpdateModel
  mapper:
    generate: true
    name: '{Entity.Name}Profile'
    baseClass: Profile
    namespace: '{Project.Namespace}.Domain.Mapping'
    directory: '{Project.Directory}\Domain\{Entity.Name}\Mapping'
  validator:
    generate: true
    name: '{Model.Name}Validator'
    baseClass: 'AbstractValidator<{Model.Name}>'
    namespace: '{Project.Namespace}.Domain.Validation'
    directory: '{Project.Directory}\Domain\{Entity.Name}\Validation'
```
