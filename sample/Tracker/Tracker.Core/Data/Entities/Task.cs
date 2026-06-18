using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Represents the <c>Task</c> entity mapped to the <c>dbo.Task</c> table.
/// </summary>
[Table("Task", Schema = "dbo")]
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
    /// Gets or sets the <c>Id</c> value mapped to the <c>Id</c> column.
    /// </summary>
    /// <value>
    /// The <c>Id</c> entity value.
    /// </value>
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the <c>StatusId</c> value mapped to the <c>StatusId</c> column.
    /// </summary>
    /// <value>
    /// The <c>StatusId</c> entity value.
    /// </value>
    [Column("StatusId", TypeName = "uniqueidentifier")]
    public Guid StatusId { get; set; }

    /// <summary>
    /// Gets or sets the <c>PriorityId</c> value mapped to the <c>PriorityId</c> column.
    /// </summary>
    /// <value>
    /// The <c>PriorityId</c> entity value.
    /// </value>
    [Column("PriorityId", TypeName = "uniqueidentifier")]
    public Guid? PriorityId { get; set; }

    /// <summary>
    /// Gets or sets the <c>Title</c> value mapped to the <c>Title</c> column.
    /// </summary>
    /// <value>
    /// The <c>Title</c> entity value.
    /// </value>
    [Column("Title", TypeName = "nvarchar(255)")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>Description</c> value mapped to the <c>Description</c> column.
    /// </summary>
    /// <value>
    /// The <c>Description</c> entity value.
    /// </value>
    [Column("Description", TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the <c>StartDate</c> value mapped to the <c>StartDate</c> column.
    /// </summary>
    /// <value>
    /// The <c>StartDate</c> entity value.
    /// </value>
    [Column("StartDate", TypeName = "datetimeoffset")]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the <c>DueDate</c> value mapped to the <c>DueDate</c> column.
    /// </summary>
    /// <value>
    /// The <c>DueDate</c> entity value.
    /// </value>
    [Column("DueDate", TypeName = "datetimeoffset")]
    public DateTimeOffset? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the <c>CompleteDate</c> value mapped to the <c>CompleteDate</c> column.
    /// </summary>
    /// <value>
    /// The <c>CompleteDate</c> entity value.
    /// </value>
    [Column("CompleteDate", TypeName = "datetimeoffset")]
    public DateTimeOffset? CompleteDate { get; set; }

    /// <summary>
    /// Gets or sets the <c>AssignedId</c> value mapped to the <c>AssignedId</c> column.
    /// </summary>
    /// <value>
    /// The <c>AssignedId</c> entity value.
    /// </value>
    [Column("AssignedId", TypeName = "uniqueidentifier")]
    public Guid? AssignedId { get; set; }

    /// <summary>
    /// Gets or sets the <c>TenantId</c> value mapped to the <c>TenantId</c> column.
    /// </summary>
    /// <value>
    /// The <c>TenantId</c> entity value.
    /// </value>
    [Column("TenantId", TypeName = "uniqueidentifier")]
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the <c>Created</c> value mapped to the <c>Created</c> column.
    /// </summary>
    /// <value>
    /// The <c>Created</c> entity value.
    /// </value>
    [Column("Created", TypeName = "datetimeoffset")]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Gets or sets the <c>CreatedBy</c> value mapped to the <c>CreatedBy</c> column.
    /// </summary>
    /// <value>
    /// The <c>CreatedBy</c> entity value.
    /// </value>
    [Column("CreatedBy", TypeName = "nvarchar(100)")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the <c>Updated</c> value mapped to the <c>Updated</c> column.
    /// </summary>
    /// <value>
    /// The <c>Updated</c> entity value.
    /// </value>
    [Column("Updated", TypeName = "datetimeoffset")]
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Gets or sets the <c>UpdatedBy</c> value mapped to the <c>UpdatedBy</c> column.
    /// </summary>
    /// <value>
    /// The <c>UpdatedBy</c> entity value.
    /// </value>
    [Column("UpdatedBy", TypeName = "nvarchar(100)")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the <c>RowVersion</c> value mapped to the <c>RowVersion</c> column.
    /// </summary>
    /// <value>
    /// The <c>RowVersion</c> entity value.
    /// </value>
    [ConcurrencyCheck]
    [Column("RowVersion", TypeName = "rowversion")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public byte[] RowVersion { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>PeriodStart1</c> value mapped to the <c>PeriodStart</c> column.
    /// </summary>
    /// <value>
    /// The <c>PeriodStart1</c> entity value.
    /// </value>
    [Column("PeriodStart", TypeName = "datetime2")]
    public DateTime PeriodStart1 { get; set; }

    /// <summary>
    /// Gets or sets the <c>PeriodEnd1</c> value mapped to the <c>PeriodEnd</c> column.
    /// </summary>
    /// <value>
    /// The <c>PeriodEnd1</c> entity value.
    /// </value>
    [Column("PeriodEnd", TypeName = "datetime2")]
    public DateTime PeriodEnd1 { get; set; }

    #endregion

    #region Generated Relationships
    /// <summary>
    /// Gets or sets the related <see cref="User" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="User" /> entity.
    /// </value>
    /// <seealso cref="AssignedId" />
    public virtual User? AssignedUser { get; set; }

    /// <summary>
    /// Gets or sets the related <see cref="Priority" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="Priority" /> entity.
    /// </value>
    /// <seealso cref="PriorityId" />
    public virtual Priority? Priority { get; set; }

    /// <summary>
    /// Gets or sets the related <see cref="Status" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="Status" /> entity.
    /// </value>
    /// <seealso cref="StatusId" />
    public virtual Status Status { get; set; } = null!;

    /// <summary>
    /// Gets or sets the related <see cref="TaskExtended" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="TaskExtended" /> entity.
    /// </value>
    /// <seealso cref="Id" />
    public virtual TaskExtended TaskExtended { get; set; } = null!;

    /// <summary>
    /// Gets or sets the related <see cref="Tenant" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="Tenant" /> entity.
    /// </value>
    /// <seealso cref="TenantId" />
    public virtual Tenant Tenant { get; set; } = null!;

    #endregion

}
