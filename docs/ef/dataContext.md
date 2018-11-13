# Data Context Template

The data context template outputs the Entity Framework `DbContext` class.

## Output

Sample generated class from this template.

```C#
public partial class TrackerContext : DbContext
{
    public TrackerContext(DbContextOptions<TrackerContext> options)
        : base(options)
    {
    }

    #region Generated Properties
    public virtual DbSet<Tracker.Core.Data.Entities.Audit> Audits { get; set; }

    public virtual DbSet<Tracker.Core.Data.Entities.Priority> Priorities { get; set; }

    public virtual DbSet<Tracker.Core.Data.Entities.Role> Roles { get; set; }

    public virtual DbSet<Tracker.Core.Data.Entities.Status> Statuses { get; set; }

    public virtual DbSet<Tracker.Core.Data.Entities.Task> Tasks { get; set; }

    public virtual DbSet<Tracker.Core.Data.Entities.User> Users { get; set; }

    public virtual DbSet<Tracker.Core.Data.Entities.TaskExtended> TaskExtended { get; set; }

    public virtual DbSet<Tracker.Core.Data.Entities.UserLogin> UserLogins { get; set; }

    public virtual DbSet<Tracker.Core.Data.Entities.UserRole> UserRoles { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Generated Configuration
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.AuditMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.PriorityMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.RoleMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.StatusMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.TaskMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.TaskExtendedMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserLoginMap());
        modelBuilder.ApplyConfiguration(new Tracker.Core.Data.Mapping.UserRoleMap());
        #endregion
    }
}
```

## Configuration

The data context template has the following configuration that can be set in the yaml [configuration file](../configuration.md).

Example configuration

```YAML
data:
  context:
    name: '{Database.Name}Context'
    baseClass: DbContext
    namespace: '{Project.Namespace}.Data'
    directory: '{Project.Directory}\Data'
    propertyNaming: Plural
```

### name

The class name the data context.  *Variables Supported*

### baseClass

The base class to inherit from.  Default `DbContext` *Variables Supported*

### namespace

The namespace for the class. *Variables Supported*

### directory

The location to write the source file. *Variables Supported*

### propertyNaming

Configuration on how to generate names for the DbSet properties on the data context.  Default: `Plural`

* **Preserve** - Keep underlying entity name as property name
* **Plural** - Use the plural form of the entity name
* **Suffix** - Add 'DataSet' to the end of the entity name

### document

Include XML documentation for the generated class.  Default: `false`

## Regeneration

The data context template has two regions that are replaced on regeneration.

### Generated Properties

The `Generated Properties` region contains all the `DbSet` properties that can be used to query and save instances of an entity.

Property rename is supported.  The rename will be discovered during the parsing phase of the source generation.

### Generated Configuration

The `Generated Configuration` region configures the entity type mapping.  