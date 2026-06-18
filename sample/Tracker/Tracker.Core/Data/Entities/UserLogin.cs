using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Represents the <c>UserLogin</c> entity mapped to the <c>dbo.UserLogin</c> table.
/// </summary>
[Table("UserLogin", Schema = "dbo")]
public partial class UserLogin
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserLogin"/> class.
    /// </summary>
    public UserLogin()
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
    /// Gets or sets the <c>EmailAddress</c> value mapped to the <c>EmailAddress</c> column.
    /// </summary>
    /// <value>
    /// The <c>EmailAddress</c> entity value.
    /// </value>
    [Column("EmailAddress", TypeName = "nvarchar(256)")]
    public string EmailAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>UserId</c> value mapped to the <c>UserId</c> column.
    /// </summary>
    /// <value>
    /// The <c>UserId</c> entity value.
    /// </value>
    [Column("UserId", TypeName = "uniqueidentifier")]
    public Guid? UserId { get; set; }

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
    /// Gets or sets the <c>DeviceFamily</c> value mapped to the <c>DeviceFamily</c> column.
    /// </summary>
    /// <value>
    /// The <c>DeviceFamily</c> entity value.
    /// </value>
    [Column("DeviceFamily", TypeName = "nvarchar(256)")]
    public string? DeviceFamily { get; set; }

    /// <summary>
    /// Gets or sets the <c>DeviceBrand</c> value mapped to the <c>DeviceBrand</c> column.
    /// </summary>
    /// <value>
    /// The <c>DeviceBrand</c> entity value.
    /// </value>
    [Column("DeviceBrand", TypeName = "nvarchar(256)")]
    public string? DeviceBrand { get; set; }

    /// <summary>
    /// Gets or sets the <c>DeviceModel</c> value mapped to the <c>DeviceModel</c> column.
    /// </summary>
    /// <value>
    /// The <c>DeviceModel</c> entity value.
    /// </value>
    [Column("DeviceModel", TypeName = "nvarchar(256)")]
    public string? DeviceModel { get; set; }

    /// <summary>
    /// Gets or sets the <c>IpAddress</c> value mapped to the <c>IpAddress</c> column.
    /// </summary>
    /// <value>
    /// The <c>IpAddress</c> entity value.
    /// </value>
    [Column("IpAddress", TypeName = "nvarchar(50)")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the <c>IsSuccessful</c> value mapped to the <c>IsSuccessful</c> column.
    /// </summary>
    /// <value>
    /// The <c>IsSuccessful</c> entity value.
    /// </value>
    [Column("IsSuccessful", TypeName = "bit")]
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the <c>FailureMessage</c> value mapped to the <c>FailureMessage</c> column.
    /// </summary>
    /// <value>
    /// The <c>FailureMessage</c> entity value.
    /// </value>
    [Column("FailureMessage", TypeName = "nvarchar(256)")]
    public string? FailureMessage { get; set; }

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
    /// Gets or sets the related <see cref="User" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="User" /> entity.
    /// </value>
    /// <seealso cref="UserId" />
    public virtual User? User { get; set; }

    #endregion

}
