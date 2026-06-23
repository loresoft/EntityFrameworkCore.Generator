using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Represents the <c>TaskExtended</c> entity mapped to the <c>dbo.TaskExtended</c> table.
/// </summary>
[Table("TaskExtended", Schema = "dbo")]
public partial class TaskExtended
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
    /// Gets or sets the <c>TaskId</c> value mapped to the <c>TaskId</c> column.
    /// </summary>
    /// <value>
    /// The <c>TaskId</c> entity value.
    /// </value>
    [Key]
    [Column("TaskId", TypeName = "uniqueidentifier")]
    public Guid TaskId { get; set; }

    /// <summary>
    /// Gets or sets the <c>UserAgent</c> value mapped to the <c>UserAgent</c> column.
    /// </summary>
    /// <value>
    /// The <c>UserAgent</c> entity value.
    /// </value>
    [Column("UserAgent", TypeName = "nvarchar(max)")]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the <c>Browser</c> value mapped to the <c>Browser</c> column.
    /// </summary>
    /// <value>
    /// The <c>Browser</c> entity value.
    /// </value>
    [Column("Browser", TypeName = "nvarchar(256)")]
    public string? Browser { get; set; }

    /// <summary>
    /// Gets or sets the <c>OperatingSystem</c> value mapped to the <c>OperatingSystem</c> column.
    /// </summary>
    /// <value>
    /// The <c>OperatingSystem</c> entity value.
    /// </value>
    [Column("OperatingSystem", TypeName = "nvarchar(256)")]
    public string? OperatingSystem { get; set; }

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

    #endregion

    #region Generated Relationships
    /// <summary>
    /// Gets or sets the related <see cref="Task" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="Task" /> entity.
    /// </value>
    /// <seealso cref="TaskId" />
    public virtual Task Task { get; set; } = null!;

    #endregion

}
