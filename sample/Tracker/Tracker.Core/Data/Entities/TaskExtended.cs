using System;
using System.Collections.Generic;
using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Entity class representing data for table 'TaskExtended'.
/// </summary>
[System.ComponentModel.DataAnnotations.Schema.Table("TaskExtended", Schema = "dbo")]
public partial class TaskExtended : ITrackCreated, ITrackUpdated
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TaskExtended"/> class.
    /// </summary>
    public TaskExtended()
    {
        #region Generated Constructor
        #endregion
    }

    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value representing column <c>TaskId</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>TaskId</c>.
    /// </value>
    [Key]
    [Column("TaskId", TypeName = "uniqueidentifier")]
    public Guid TaskId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>UserAgent</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>UserAgent</c>.
    /// </value>
    [Column("UserAgent", TypeName = "nvarchar(max)")]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Browser</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Browser</c>.
    /// </value>
    [Column("Browser", TypeName = "nvarchar(256)")]
    public string? Browser { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>OperatingSystem</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>OperatingSystem</c>.
    /// </value>
    [Column("OperatingSystem", TypeName = "nvarchar(256)")]
    public string? OperatingSystem { get; set; }

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

    #endregion

    #region Generated Relationships
    /// <summary>
    /// Gets or sets the navigation property for entity <see cref="Task" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="Task" />.
    /// </value>
    /// <seealso cref="TaskId" />
    public virtual Task Task { get; set; } = null!;

    #endregion

}
