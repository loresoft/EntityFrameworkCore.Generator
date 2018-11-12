# Entity Mapping Template

The entity mapping class template.  An entity mapping class is created for each table in the context.  The class is used to map table to entity and column to property using the Entity Framework fluent syntax

## Output

Example of a generated entity mapping class

```C#
public partial class TaskMap
    : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("Task", "dbo");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(t => t.StatusId)
            .IsRequired()
            .HasColumnName("StatusId")
            .HasColumnType("int");

        builder.Property(t => t.PriorityId)
            .HasColumnName("PriorityId")
            .HasColumnType("int");

        builder.Property(t => t.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("nvarchar(255)")
            .HasMaxLength(255);

        builder.Property(t => t.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.StartDate)
            .HasColumnName("StartDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.DueDate)
            .HasColumnName("DueDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.CompleteDate)
            .HasColumnName("CompleteDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.AssignedId)
            .HasColumnName("AssignedId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.Created)
            .IsRequired()
            .HasColumnName("Created")
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.CreatedBy)
            .HasColumnName("CreatedBy")
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.Updated)
            .IsRequired()
            .HasColumnName("Updated")
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.UpdatedBy)
            .HasColumnName("UpdatedBy")
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.RowVersion)
            .IsRequired()
            .IsRowVersion()
            .HasColumnName("RowVersion")
            .HasColumnType("rowversion")
            .ValueGeneratedOnAddOrUpdate();

        // relationships
        builder.HasOne(t => t.Priority)
            .WithMany(t => t.Tasks)
            .HasForeignKey(d => d.PriorityId)
            .HasConstraintName("FK_Task_Priority_PriorityId");

        builder.HasOne(t => t.Status)
            .WithMany(t => t.Tasks)
            .HasForeignKey(d => d.StatusId)
            .HasConstraintName("FK_Task_Status_StatusId");

        builder.HasOne(t => t.AssignedUser)
            .WithMany(t => t.AssignedTasks)
            .HasForeignKey(d => d.AssignedId)
            .HasConstraintName("FK_Task_User_AssignedId");

        #endregion
    }
}
```

## Configuration

The entity template has the following configuration that can be set in the yaml [configuration file](../configuration.md).

### namespace

The namespace for the class. *Variables Supported*

### directory

The directory location to write the source file. *Variables Supported*

### document

Include XML documentation for the generated class.  Default: `false`

## Regeneration

The entity template has one region that is replaced on regeneration.

### Generated Configuration

The `Generated Configure` region configures the entity type mapping.  
