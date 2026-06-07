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
    /// Gets or sets the property value representing column 'Id'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Id'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Key()]
    [System.ComponentModel.DataAnnotations.Schema.Column("Id", TypeName = "UNIQUEIDENTIFIER")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'Date'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Date'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Date", TypeName = "DATETIME")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'UserId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'UserId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("UserId", TypeName = "UNIQUEIDENTIFIER")]
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'TaskId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'TaskId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("TaskId", TypeName = "UNIQUEIDENTIFIER")]
    public Guid? TaskId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'Content'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Content'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Content", TypeName = "NVARCHAR(MAX)")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column 'Username'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Username'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Username", TypeName = "NVARCHAR(50)")]
    public string Username { get; set; } = null!;

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

    #endregion

    #region Generated Relationships
    #endregion

}
