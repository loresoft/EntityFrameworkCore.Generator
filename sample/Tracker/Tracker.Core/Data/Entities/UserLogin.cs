using System;
using System.Collections.Generic;
using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Entity class representing data for table 'UserLogin'.
/// </summary>
[System.ComponentModel.DataAnnotations.Schema.Table("UserLogin", Schema = "dbo")]
public partial class UserLogin : IHaveIdentifier, ITrackCreated, ITrackUpdated
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
    /// Gets or sets the property value representing column <c>Id</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>Id</c>.
    /// </value>
    [Key]
    [Column("Id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>EmailAddress</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>EmailAddress</c>.
    /// </value>
    [Column("EmailAddress", TypeName = "nvarchar(256)")]
    public string EmailAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column <c>UserId</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>UserId</c>.
    /// </value>
    [Column("UserId", TypeName = "uniqueidentifier")]
    public Guid? UserId { get; set; }

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
    /// Gets or sets the property value representing column <c>DeviceFamily</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>DeviceFamily</c>.
    /// </value>
    [Column("DeviceFamily", TypeName = "nvarchar(256)")]
    public string? DeviceFamily { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>DeviceBrand</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>DeviceBrand</c>.
    /// </value>
    [Column("DeviceBrand", TypeName = "nvarchar(256)")]
    public string? DeviceBrand { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>DeviceModel</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>DeviceModel</c>.
    /// </value>
    [Column("DeviceModel", TypeName = "nvarchar(256)")]
    public string? DeviceModel { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>IpAddress</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>IpAddress</c>.
    /// </value>
    [Column("IpAddress", TypeName = "nvarchar(50)")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>IsSuccessful</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>IsSuccessful</c>.
    /// </value>
    [Column("IsSuccessful", TypeName = "bit")]
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>FailureMessage</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>FailureMessage</c>.
    /// </value>
    [Column("FailureMessage", TypeName = "nvarchar(256)")]
    public string? FailureMessage { get; set; }

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
    /// Gets or sets the navigation property for entity <see cref="User" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="User" />.
    /// </value>
    /// <seealso cref="UserId" />
    public virtual User? User { get; set; }

    #endregion

}
