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
    /// Gets or sets the property value representing column 'Id'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Id'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Key()]
    [System.ComponentModel.DataAnnotations.Schema.Column("Id", TypeName = "UNIQUEIDENTIFIER")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'StatusId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'StatusId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("StatusId", TypeName = "UNIQUEIDENTIFIER")]
    public Guid StatusId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'PriorityId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'PriorityId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("PriorityId", TypeName = "UNIQUEIDENTIFIER")]
    public Guid? PriorityId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'Title'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Title'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Title", TypeName = "NVARCHAR(255)")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column 'Description'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Description'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Description", TypeName = "NVARCHAR(MAX)")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'StartDate'.
    /// </summary>
    /// <value>
    /// The property value representing column 'StartDate'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("StartDate", TypeName = "DATETIMEOFFSET")]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'DueDate'.
    /// </summary>
    /// <value>
    /// The property value representing column 'DueDate'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("DueDate", TypeName = "DATETIMEOFFSET")]
    public DateTimeOffset? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'CompleteDate'.
    /// </summary>
    /// <value>
    /// The property value representing column 'CompleteDate'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("CompleteDate", TypeName = "DATETIMEOFFSET")]
    public DateTimeOffset? CompleteDate { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'AssignedId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'AssignedId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("AssignedId", TypeName = "UNIQUEIDENTIFIER")]
    public Guid? AssignedId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'TenantId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'TenantId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("TenantId", TypeName = "UNIQUEIDENTIFIER")]
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'Created'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Created'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Created", TypeName = "DATETIMEOFFSET")]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'CreatedBy'.
    /// </summary>
    /// <value>
    /// The property value representing column 'CreatedBy'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("CreatedBy", TypeName = "NVARCHAR(100)")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'Updated'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Updated'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Updated", TypeName = "DATETIMEOFFSET")]
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'UpdatedBy'.
    /// </summary>
    /// <value>
    /// The property value representing column 'UpdatedBy'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("UpdatedBy", TypeName = "NVARCHAR(100)")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'RowVersion'.
    /// </summary>
    /// <value>
    /// The property value representing column 'RowVersion'.
    /// </value>
    [System.ComponentModel.DataAnnotations.ConcurrencyCheck()]
    [System.ComponentModel.DataAnnotations.Schema.Column("RowVersion", TypeName = "TIMESTAMP")]
    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)]
    public byte[] RowVersion { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column 'PeriodStart'.
    /// </summary>
    /// <value>
    /// The property value representing column 'PeriodStart'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("PeriodStart", TypeName = "DATETIME2")]
    public DateTime PeriodStart1 { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'PeriodEnd'.
    /// </summary>
    /// <value>
    /// The property value representing column 'PeriodEnd'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("PeriodEnd", TypeName = "DATETIME2")]
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
