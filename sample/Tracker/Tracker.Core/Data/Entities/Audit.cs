using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Represents the <c>Audit</c> entity mapped to the <c>dbo.Audit</c> table.
/// </summary>
[Table("Audit", Schema = "dbo")]
public partial class Audit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Audit"/> class.
    /// </summary>
    public Audit()
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
    /// Gets or sets the <c>Date</c> value mapped to the <c>Date</c> column.
    /// </summary>
    /// <value>
    /// The <c>Date</c> entity value.
    /// </value>
    [Column("Date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the <c>UserId</c> value mapped to the <c>UserId</c> column.
    /// </summary>
    /// <value>
    /// The <c>UserId</c> entity value.
    /// </value>
    [Column("UserId", TypeName = "uniqueidentifier")]
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the <c>TaskId</c> value mapped to the <c>TaskId</c> column.
    /// </summary>
    /// <value>
    /// The <c>TaskId</c> entity value.
    /// </value>
    [Column("TaskId", TypeName = "uniqueidentifier")]
    public Guid? TaskId { get; set; }

    /// <summary>
    /// Gets or sets the <c>Content</c> value mapped to the <c>Content</c> column.
    /// </summary>
    /// <value>
    /// The <c>Content</c> entity value.
    /// </value>
    [Column("Content", TypeName = "nvarchar(max)")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>Username</c> value mapped to the <c>Username</c> column.
    /// </summary>
    /// <value>
    /// The <c>Username</c> entity value.
    /// </value>
    [Column("Username", TypeName = "nvarchar(50)")]
    public string Username { get; set; } = null!;

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
    /// Gets or sets the <c>Attributes</c> value mapped to the <c>Attributes</c> column.
    /// </summary>
    /// <value>
    /// The <c>Attributes</c> entity value.
    /// </value>
    [Column("Attributes", TypeName = "StringList")]
    public string? Attributes { get; set; }

    #endregion

    #region Generated Relationships
    #endregion

}
