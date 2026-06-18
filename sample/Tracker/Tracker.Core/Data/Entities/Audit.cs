using System;
using System.Collections.Generic;
using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Entity class representing data for table 'Audit'.
/// </summary>
[System.ComponentModel.DataAnnotations.Schema.Table("Audit", Schema = "dbo")]
public partial class Audit : IHaveIdentifier
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
    /// Gets or sets the property value representing column <c>Id</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Id</c>.
    /// </value>
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Date</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Date</c>.
    /// </value>
    [Column("Date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>UserId</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>UserId</c>.
    /// </value>
    [Column("UserId", TypeName = "uniqueidentifier")]
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>TaskId</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>TaskId</c>.
    /// </value>
    [Column("TaskId", TypeName = "uniqueidentifier")]
    public Guid? TaskId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>Content</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Content</c>.
    /// </value>
    [Column("Content", TypeName = "nvarchar(max)")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column <c>Username</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Username</c>.
    /// </value>
    [Column("Username", TypeName = "nvarchar(50)")]
    public string Username { get; set; } = null!;

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
    /// Gets or sets the property value representing column <c>Attributes</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Attributes</c>.
    /// </value>
    [Column("Attributes", TypeName = "StringList")]
    public string? Attributes { get; set; }

    #endregion

    #region Generated Relationships
    #endregion

}
