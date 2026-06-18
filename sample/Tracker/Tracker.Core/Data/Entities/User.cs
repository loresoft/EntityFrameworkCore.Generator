using System;
using System.Collections.Generic;
using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Entity class representing data for table 'User'.
/// </summary>
[System.ComponentModel.DataAnnotations.Schema.Table("User", Schema = "dbo")]
public partial class User : IHaveIdentifier, ITrackCreated, ITrackUpdated
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
    /// Gets or sets the property value representing column <c>IsEmailAddressConfirmed</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>IsEmailAddressConfirmed</c>.
    /// </value>
    [Column("IsEmailAddressConfirmed", TypeName = "bit")]
    public bool IsEmailAddressConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>DisplayName</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>DisplayName</c>.
    /// </value>
    [Column("DisplayName", TypeName = "nvarchar(256)")]
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value representing column <c>PasswordHash</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>PasswordHash</c>.
    /// </value>
    [Column("PasswordHash", TypeName = "nvarchar(max)")]
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>ResetHash</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>ResetHash</c>.
    /// </value>
    [Column("ResetHash", TypeName = "nvarchar(max)")]
    public string? ResetHash { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>InviteHash</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>InviteHash</c>.
    /// </value>
    [Column("InviteHash", TypeName = "nvarchar(max)")]
    public string? InviteHash { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>AccessFailedCount</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>AccessFailedCount</c>.
    /// </value>
    [Column("AccessFailedCount", TypeName = "int")]
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>LockoutEnabled</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>LockoutEnabled</c>.
    /// </value>
    [Column("LockoutEnabled", TypeName = "bit")]
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>LockoutEnd</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>LockoutEnd</c>.
    /// </value>
    [Column("LockoutEnd", TypeName = "datetimeoffset")]
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>LastLogin</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>LastLogin</c>.
    /// </value>
    [Column("LastLogin", TypeName = "datetimeoffset")]
    public DateTimeOffset? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column <c>IsDeleted</c>.
    /// </summary>
    /// <value>
    /// The property value representing column <c>IsDeleted</c>.
    /// </value>
    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

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
