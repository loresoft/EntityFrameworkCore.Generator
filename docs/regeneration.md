# Regeneration

Entity Framework Core Generator supports safe regeneration via region replacement and source code parsing.  A typical workflow for a project requires many database changes and updates.  Being able to regenerate the entities and associated files is a huge time saver. 

## Region Replacement

All the templates output a region on first generation.  On future regeneration, only the regions are replaced.  This keeps any other changes you've made the the source file.

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

When the `generate` command is re-run, the `Generated Constructor`, `Generated Properties` and `Generated Relationships` regions will be replace with the current output of the template.  Any other changes outside those regions will be safe.

## Source Parsing

In order to capture and preserve Entity, Property and DbContext renames, the `generate` command parses any existing mapping and DbContext class to capture how things are named.  This allows you to use the full extend of Visual Studio's refactoring tools to rename things as you like.  Then, when regenerating, those changes won't be lost.