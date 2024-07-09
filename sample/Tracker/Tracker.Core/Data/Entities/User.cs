using System;
using System.Collections.Generic;
using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Entity class representing data for table 'User'.
/// </summary>
[System.ComponentModel.DataAnnotations.Schema.Table("User", Schema = "dbo")]
partial class User : IHaveIdentifier, ITrackCreated, ITrackUpdated
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    public User()
    {
        #region Generated Constructor
        AssignedTasks = new HashSet<Task>();
        UserLogins = new HashSet<UserLogin>();
        UserRoles = new HashSet<UserRole>();
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
    /// Gets or sets the property value representing column 'IsEmailAddressConfirmed'.
    /// </summary>
    /// <value>
    /// The property value representing column 'IsEmailAddressConfirmed'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("IsEmailAddressConfirmed", TypeName = "bit")]
    public bool IsEmailAddressConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'DisplayName'.
    /// </summary>
    /// <value>
    /// The property value representing column 'DisplayName'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("DisplayName", TypeName = "nvarchar(256)")]
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column 'PasswordHash'.
    /// </summary>
    /// <value>
    /// The property value representing column 'PasswordHash'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("PasswordHash", TypeName = "nvarchar(max)")]
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'ResetHash'.
    /// </summary>
    /// <value>
    /// The property value representing column 'ResetHash'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("ResetHash", TypeName = "nvarchar(max)")]
    public string? ResetHash { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'InviteHash'.
    /// </summary>
    /// <value>
    /// The property value representing column 'InviteHash'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("InviteHash", TypeName = "nvarchar(max)")]
    public string? InviteHash { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'AccessFailedCount'.
    /// </summary>
    /// <value>
    /// The property value representing column 'AccessFailedCount'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("AccessFailedCount", TypeName = "int")]
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'LockoutEnabled'.
    /// </summary>
    /// <value>
    /// The property value representing column 'LockoutEnabled'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("LockoutEnabled", TypeName = "bit")]
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'LockoutEnd'.
    /// </summary>
    /// <value>
    /// The property value representing column 'LockoutEnd'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("LockoutEnd", TypeName = "datetimeoffset")]
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'LastLogin'.
    /// </summary>
    /// <value>
    /// The property value representing column 'LastLogin'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("LastLogin", TypeName = "datetimeoffset")]
    public DateTimeOffset? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'IsDeleted'.
    /// </summary>
    /// <value>
    /// The property value representing column 'IsDeleted'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Schema.Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

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
    /// Gets or sets the navigation collection for entity <see cref="Task" />.
    /// </summary>
    /// <value>
    /// The navigation collection for entity <see cref="Task" />.
    /// </value>
    public virtual ICollection<Task> AssignedTasks { get; set; }

    /// <summary>
    /// Gets or sets the navigation collection for entity <see cref="UserLogin" />.
    /// </summary>
    /// <value>
    /// The navigation collection for entity <see cref="UserLogin" />.
    /// </value>
    public virtual ICollection<UserLogin> UserLogins { get; set; }

    /// <summary>
    /// Gets or sets the navigation collection for entity <see cref="UserRole" />.
    /// </summary>
    /// <value>
    /// The navigation collection for entity <see cref="UserRole" />.
    /// </value>
    public virtual ICollection<UserRole> UserRoles { get; set; }

    #endregion

}
