# Query Extensions Template

Query Extension method template.  This template create an extension method for each key or index on a table.  

## Output

Example of a generated query extension class

```C#
public static partial class TaskExtensions
{
    #region Generated Extensions
    public static Task GetByKey(this IQueryable<Task> queryable, Guid id)
    {
        if (queryable is DbSet<Task> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    public static Task<Task> GetByKeyAsync(this IQueryable<Task> queryable, Guid id)
    {
        if (queryable is DbSet<Task> dbSet)
            return dbSet.FindAsync(id);

        return queryable.FirstOrDefaultAsync(q => q.Id == id);
    }

    public static IQueryable<Task> ByAssignedId(this IQueryable<Task> queryable, Guid? assignedId)
    {
        return queryable.Where(q => (q.AssignedId == assignedId || (assignedId == null && q.AssignedId == null)));
    }

    public static IQueryable<Task> ByPriorityId(this IQueryable<Task> queryable, int? priorityId)
    {
        return queryable.Where(q => (q.PriorityId == priorityId || (priorityId == null && q.PriorityId == null)));
    }

    public static IQueryable<Task> ByStatusId(this IQueryable<Task> queryable, int statusId)
    {
        return queryable.Where(q => q.StatusId == statusId);
    }

    #endregion
}

```

## Configuration

The query extension class has the following configuration that can be set in the yaml [configuration file](../configuration.md).

### generate

Flag to enable generating the output for this template.  Default: `true`

### namespace

The namespace for the class. *Variables Supported*

### directory

The directory location to write the source file. *Variables Supported*

### indexPrefix

Prefix for queries built from an index

### uniquePrefix

Prefix for queries built from unique indexes

### document

Include XML documentation for the generated class.  Default: `false`

## Regeneration

The query extension template has one region that is replaced on regeneration.

### Generated Configuration

The `Generated Configure` region configures the entity type mapping.  