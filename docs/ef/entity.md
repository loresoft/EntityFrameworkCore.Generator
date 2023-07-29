# Entity Template

The entity class template. An entity is created for each table in the context.

## Output

Example of a generated entity class

```C#
public partial class Status
{
    public Status()
    {
        #region Generated Constructor
        Tasks = new HashSet<Task>();
        #endregion
    }

    #region Generated Properties
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string UpdatedBy { get; set; }

    public Byte[] RowVersion { get; set; }
    #endregion

    #region Generated Relationships
    public virtual ICollection<Task> Tasks { get; set; }
    #endregion
}
```

## Configuration

The entity template has the following configuration that can be set in the yaml [configuration file](../configuration.md).

Example configuration

```YAML
data:
  entity:
    namespace: '{Project.Namespace}.Data.Entities'
    directory: '{Project.Directory}\Data\Entities'
    entityNaming: Singular
    relationshipNaming: Plural
    prefixWithSchemaName: false
    renaming:
      entities:
        - ^(sp|tbl|udf|vw)_
      properties:
        - ^{Table.Name}(?=Id|Name)    
```

### name

The class name of the entity.  Will be generated if null or empty.  _Variables Supported_

### namespace

The namespace for the class. _Variables Supported_

### directory

The directory location to write the source file. _Variables Supported_

### entityNaming

Control how to generate entity class names from the table name. Default: `Singular`

- **Preserve** - Keep table name as entity name
- **Plural** - Use the plural form of the table name
- **Singular** - Use the singular form of the table name

### relationshipNaming

Configuration on how to generate relationship property names. Default: `Plural`

- **Preserve** - Keep underlying entity name as property name
- **Plural** - Use the plural form of the entity name
- **Suffix** - Add 'List' to the end of the entity name

### prefixWithSchemaName

**Obsolete** Use the name option for more flexibility.

Control if class names should be generated with schema name prefixed eg. dbo.MyTable = DboMyTable. Default: `false`

### document

Include XML documentation for the generated class. Default: `false`

### renaming

Rename entities and properties with regular expressions

#### entities

list of regular expressions to clean entity names

#### properties

list of regular expressions to clean property names

## Regeneration

The entity template has three regions that are replaced on regeneration.

### Generated Constructor

The `Generated Constructor` region initializes any relationship collection in the constructor.

### Generated Properties

The `Generated Properties` region contains all the properties that are mapped to columns for the entity.

Property rename is supported. The rename will be discovered during the parsing phase of the source generation.

### Generated Relationships

The `Generated Relationships` region contains all the relationship navigation properties.

Property rename is supported. The rename will be discovered during the parsing phase of the source generation.
