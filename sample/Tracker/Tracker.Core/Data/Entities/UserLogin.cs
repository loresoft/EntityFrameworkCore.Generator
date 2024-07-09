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
    /// Gets or sets the property value representing column 'Id'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Id'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Key()]
    [System.ComponentModel.DataAnnotations.Schema.Column("Id", TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'EmailAddress'.
    /// </summary>
    /// <value>
    /// The property value representing column 'EmailAddress'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("EmailAddress", TypeName = "nvarchar(256)")]
    public string EmailAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column 'UserId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'UserId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("UserId", TypeName = "uniqueidentifier")]
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'UserAgent'.
    /// </summary>
    /// <value>
    /// The property value representing column 'UserAgent'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("UserAgent", TypeName = "nvarchar(max)")]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'Browser'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Browser'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Browser", TypeName = "nvarchar(256)")]
    public string? Browser { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'OperatingSystem'.
    /// </summary>
    /// <value>
    /// The property value representing column 'OperatingSystem'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("OperatingSystem", TypeName = "nvarchar(256)")]
    public string? OperatingSystem { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'DeviceFamily'.
    /// </summary>
    /// <value>
    /// The property value representing column 'DeviceFamily'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("DeviceFamily", TypeName = "nvarchar(256)")]
    public string? DeviceFamily { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'DeviceBrand'.
    /// </summary>
    /// <value>
    /// The property value representing column 'DeviceBrand'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("DeviceBrand", TypeName = "nvarchar(256)")]
    public string? DeviceBrand { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'DeviceModel'.
    /// </summary>
    /// <value>
    /// The property value representing column 'DeviceModel'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("DeviceModel", TypeName = "nvarchar(256)")]
    public string? DeviceModel { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'IpAddress'.
    /// </summary>
    /// <value>
    /// The property value representing column 'IpAddress'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("IpAddress", TypeName = "nvarchar(50)")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'IsSuccessful'.
    /// </summary>
    /// <value>
    /// The property value representing column 'IsSuccessful'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("IsSuccessful", TypeName = "bit")]
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'FailureMessage'.
    /// </summary>
    /// <value>
    /// The property value representing column 'FailureMessage'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("FailureMessage", TypeName = "nvarchar(256)")]
    public string? FailureMessage { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'Created'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Created'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Created", TypeName = "datetimeoffset")]
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'CreatedBy'.
    /// </summary>
    /// <value>
    /// The property value representing column 'CreatedBy'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("CreatedBy", TypeName = "nvarchar(100)")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'Updated'.
    /// </summary>
    /// <value>
    /// The property value representing column 'Updated'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("Updated", TypeName = "datetimeoffset")]
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'UpdatedBy'.
    /// </summary>
    /// <value>
    /// The property value representing column 'UpdatedBy'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("UpdatedBy", TypeName = "nvarchar(100)")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'RowVersion'.
    /// </summary>
    /// <value>
    /// The property value representing column 'RowVersion'.
    /// </value>
    [System.ComponentModel.DataAnnotations.ConcurrencyCheck()]
    [System.ComponentModel.DataAnnotations.Schema.Column("RowVersion", TypeName = "rowversion")]
    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)]
    public Byte[] RowVersion { get; set; } = null!;

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
