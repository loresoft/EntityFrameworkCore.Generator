# Script Templates

Entity Framework Core Generator supports external csx script templates.  Script templates allow you to create custom templates to generate code based on the database model.

## Script Global Variables

The script execution context contains the following global variables

### GeneratorOptions

The generator options containing all the setting for the generator.

### TemplateOptions

Options for the current template loaded from the overall generator options

### CodeBuilder

A string builder that supports indentation for writing code

## Script Types

### Context Script Template

Context script templates have a global variable of `EntityContext` that contains the full database model.  Use this script type to generate something that applies to the full model.

### Entity Script Template

Entity script templates have a global variable of `Entity` that contains the current entity being generated.  Use this script type to generate something that applies to each entity.

Example Entity Template

```c#
public string WriteCode()
{
    CodeBuilder.Clear();

    CodeBuilder.Append("EntityClass: ").Append(Entity.EntityClass).AppendLine();
    CodeBuilder.Append("EntityNamespace: '").Append(Entity.EntityNamespace).AppendLine("'");
    CodeBuilder.Append("EntityBaseClass: ").Append(Entity.EntityBaseClass).AppendLine();

    CodeBuilder.Append("ContextProperty: ").Append(Entity.ContextProperty).AppendLine();

    CodeBuilder.Append("TableSchema: '").Append(Entity.TableSchema).AppendLine("'");
    CodeBuilder.Append("TableName: '").Append(Entity.TableName).AppendLine("'");


    CodeBuilder.Append("MappingClass: ").Append(Entity.MappingClass).AppendLine();
    CodeBuilder.Append("MappingNamespace: '").Append(Entity.MappingNamespace).AppendLine("'");

    CodeBuilder.Append("MapperClass: ").Append(Entity.MapperClass).AppendLine();
    CodeBuilder.Append("MapperNamespace: '").Append(Entity.MapperClass).AppendLine("'");
    CodeBuilder.Append("MapperBaseClass: ").Append(Entity.MapperBaseClass).AppendLine();

    CodeBuilder.Append("IsView: ").Append(Entity.IsView).AppendLine();

    CodeBuilder.Append("Properties:").AppendLine();
    using (CodeBuilder.Indent())
        GenerateProperties();

    return CodeBuilder.ToString();
}

private void GenerateProperties()
{
    foreach (var property in Entity.Properties)
    {
        CodeBuilder.Append("- PropertyName: ").Append(property.PropertyName).AppendLine();
        CodeBuilder.Append("  ColumnName: '").Append(property.ColumnName).AppendLine("'");
        CodeBuilder.Append("  StoreType: ").Append(property.StoreType).AppendLine();
        CodeBuilder.Append("  NativeType: '").Append(property.NativeType).AppendLine("'");
        CodeBuilder.Append("  DataType: ").Append(property.DataType).AppendLine();
        CodeBuilder.Append("  SystemType: ").Append(property.SystemType.Name).AppendLine();

        if (property.Size != null)
            CodeBuilder.Append("  Size: ").Append(property.Size).AppendLine();

        if (property.Default != null)
            CodeBuilder.Append("  Default: '").Append(property.Default).AppendLine("'");

        if (property.ValueGenerated != null)
            CodeBuilder.Append("  ValueGenerated: ").Append(property.ValueGenerated).AppendLine();

        if (property.IsNullable != null)
            CodeBuilder.Append("  IsNullable: ").Append(property.IsNullable).AppendLine();

        if (property.IsPrimaryKey != null)
            CodeBuilder.Append("  IsPrimaryKey: ").Append(property.IsPrimaryKey).AppendLine();

        if (property.IsForeignKey != null)
            CodeBuilder.Append("  IsForeignKey: ").Append(property.IsForeignKey).AppendLine();

        if (property.IsReadOnly != null)
            CodeBuilder.Append("  IsReadOnly: ").Append(property.IsReadOnly).AppendLine();

        if (property.IsRowVersion != null)
            CodeBuilder.Append("  IsRowVersion: ").Append(property.IsRowVersion).AppendLine();

        if (property.IsUnique != null)
            CodeBuilder.Append("  IsUnique: ").Append(property.IsUnique).AppendLine();
    }
}

// run script
WriteCode()
```

### Model Script Template

Model script templates have a global variable of `Model` that contains the current model being generated.  Use this script type to generate something that applies to each model.

Example Model Template

```c#
public string WriteCode()
{
    CodeBuilder.Clear();

    CodeBuilder.Append("ModelClass: ").Append(Model.ModelClass).AppendLine();
    CodeBuilder.Append("ModelType: ").Append(Model.ModelType).AppendLine();
    CodeBuilder.Append("ModelNamespace: '").Append(Model.ModelNamespace).AppendLine("'");
    CodeBuilder.Append("ModelBaseClass: ").Append(Model.ModelBaseClass).AppendLine();
    CodeBuilder.Append("ValidatorNamespace: '").Append(Model.ValidatorNamespace).AppendLine("'");
    CodeBuilder.Append("ValidatorClass: ").Append(Model.ValidatorClass).AppendLine();
    CodeBuilder.Append("ValidatorBaseClass: ").Append(Model.ValidatorBaseClass).AppendLine();

    CodeBuilder.Append("Properties:").AppendLine();
    using (CodeBuilder.Indent())
        GenerateProperties();

    return CodeBuilder.ToString();
}

private void GenerateProperties()
{
    foreach (var property in Model.Properties)
    {
        CodeBuilder.Append("- PropertyName: ").Append(property.PropertyName).AppendLine();
        CodeBuilder.Append("  ColumnName: '").Append(property.ColumnName).AppendLine("'");
        CodeBuilder.Append("  StoreType: ").Append(property.StoreType).AppendLine();
        CodeBuilder.Append("  NativeType: '").Append(property.NativeType).AppendLine("'");
        CodeBuilder.Append("  DataType: ").Append(property.DataType).AppendLine();
        CodeBuilder.Append("  SystemType: ").Append(property.SystemType.Name).AppendLine();

        if (property.Size != null)
            CodeBuilder.Append("  Size: ").Append(property.Size).AppendLine();

        if (property.Default != null)
            CodeBuilder.Append("  Default: '").Append(property.Default).AppendLine("'");

        if (property.ValueGenerated != null)
            CodeBuilder.Append("  ValueGenerated: ").Append(property.ValueGenerated).AppendLine();

        if (property.IsNullable != null)
            CodeBuilder.Append("  IsNullable: ").Append(property.IsNullable).AppendLine();

        if (property.IsPrimaryKey != null)
            CodeBuilder.Append("  IsPrimaryKey: ").Append(property.IsPrimaryKey).AppendLine();

        if (property.IsForeignKey != null)
            CodeBuilder.Append("  IsForeignKey: ").Append(property.IsForeignKey).AppendLine();

        if (property.IsReadOnly != null)
            CodeBuilder.Append("  IsReadOnly: ").Append(property.IsReadOnly).AppendLine();

        if (property.IsRowVersion != null)
            CodeBuilder.Append("  IsRowVersion: ").Append(property.IsRowVersion).AppendLine();

        if (property.IsUnique != null)
            CodeBuilder.Append("  IsUnique: ").Append(property.IsUnique).AppendLine();
    }
}

// run script
WriteCode()
```

## Configuration

Example configuration

```YAML
script:
  entity:
    - templatePath: '.\Templates\entity-yaml.csx'
      fileName: '{Entity.Name}.yml'
      directory: '{Project.Directory}\Yaml\Entity'
      overwrite: true
  model:
    - templatePath: '.\Templates\model-yaml.csx'
      fileName: '{Model.Name}.yml'
      directory: '{Project.Directory}\Yaml\Model'
      overwrite: true
```

### TemplatePath

The file path to the script template. *Variables Supported*

### FileName

The file name to save script output. *Variables Supported*

### Namespace

The namespace for the script template. *Variables Supported*

#### BaseClass

The base class for the script template.  *Variables Supported*

#### Directory

The directory location to write script template output. *Variables Supported*

#### Overwrite

Flag indicating whether to overwrite existing file. Default: `false`
