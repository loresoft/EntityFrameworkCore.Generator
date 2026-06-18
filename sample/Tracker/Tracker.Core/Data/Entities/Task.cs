using System;

using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Entity class representing data for table 'Task'.
/// </summary>
[System.ComponentModel.DataAnnotations.Schema.Table("Task", Schema = "dbo")]
public partial class Task : IHaveIdentifier, ITrackCreated, ITrackUpdated
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Task"/> class.
    /// </summary>
    public Task()
    {
        #region Generated Constructor
        #endregion
    }

    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value representing column <c>Id</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Id</c>.
    /// </value>
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>StatusId</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>StatusId</c>.
    /// </value>
    [Column("StatusId", TypeName = "uniqueidentifier")]
    public Guid StatusId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>PriorityId</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>PriorityId</c>.
    /// </value>
    [Column("PriorityId", TypeName = "uniqueidentifier")]
    public Guid? PriorityId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Title</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Title</c>.
    /// </value>
    [Column("Title", TypeName = "nvarchar(255)")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column <c>Description</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Description</c>.
    /// </value>
    [Column("Description", TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>StartDate</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>StartDate</c>.
    /// </value>
    [Column("StartDate", TypeName = "datetimeoffset")]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>DueDate</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>DueDate</c>.
    /// </value>
    [Column("DueDate", TypeName = "datetimeoffset")]
    public DateTimeOffset? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>CompleteDate</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>CompleteDate</c>.
    /// </value>
    [Column("CompleteDate", TypeName = "datetimeoffset")]
    public DateTimeOffset? CompleteDate { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>AssignedId</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>AssignedId</c>.
    /// </value>
    [Column("AssignedId", TypeName = "uniqueidentifier")]
    public Guid? AssignedId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>TenantId</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>TenantId</c>.
    /// </value>
    [Column("TenantId", TypeName = "uniqueidentifier")]
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Created</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Created</c>.
    /// </value>
    [Column("Created", TypeName = "datetimeoffset")]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>CreatedBy</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>CreatedBy</c>.
    /// </value>
    [Column("CreatedBy", TypeName = "nvarchar(100)")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Updated</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Updated</c>.
    /// </value>
    [Column("Updated", TypeName = "datetimeoffset")]
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>UpdatedBy</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>UpdatedBy</c>.
    /// </value>
    [Column("UpdatedBy", TypeName = "nvarchar(100)")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>RowVersion</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>RowVersion</c>.
    /// </value>
    [ConcurrencyCheck]
    [Column("RowVersion", TypeName = "rowversion")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public byte[] RowVersion { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column <c>PeriodStart</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>PeriodStart</c>.
    /// </value>
    [Column("PeriodStart", TypeName = "datetime2")]
    public DateTime PeriodStart1 { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>PeriodEnd</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>PeriodEnd</c>.
    /// </value>
    [Column("PeriodEnd", TypeName = "datetime2")]
    public DateTime PeriodEnd1 { get; set; }

    #endregion

    #region Generated Relationships
    /// <summary>
    /// Gets or sets the navigation property for entity <see cref="User" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="User" />.
    /// </value>
    /// <seealso cref="AssignedId" />
    public virtual User? AssignedUser { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for entity <see cref="Priority" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="Priority" />.
    /// </value>
    /// <seealso cref="PriorityId" />
    public virtual Priority? Priority { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for entity <see cref="Status" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="Status" />.
    /// </value>
    /// <seealso cref="StatusId" />
    public virtual Status Status { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation property for entity <see cref="TaskExtended" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="TaskExtended" />.
    /// </value>
    /// <seealso cref="Id" />
    public virtual TaskExtended TaskExtended { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation property for entity <see cref="Tenant" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="Tenant" />.
    /// </value>
    /// <seealso cref="TenantId" />
    public virtual Tenant Tenant { get; set; } = null!;

    #endregion

}
