using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Represents the <c>User</c> entity mapped to the <c>dbo.User</c> table.
/// </summary>
[Table("User", Schema = "dbo")]
public partial class User : IHaveIdentifier, ITrackCreated, ITrackUpdated
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class and its collection navigation properties.
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
    /// Gets or sets the <c>IsEmailAddressConfirmed</c> value mapped to the <c>IsEmailAddressConfirmed</c> column.
    /// </summary>
    /// <value>
    /// The <c>IsEmailAddressConfirmed</c> entity value.
    /// </value>
    [Column("IsEmailAddressConfirmed", TypeName = "bit")]
    public bool IsEmailAddressConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the <c>DisplayName</c> value mapped to the <c>DisplayName</c> column.
    /// </summary>
    /// <value>
    /// The <c>DisplayName</c> entity value.
    /// </value>
    [Column("DisplayName", TypeName = "nvarchar(256)")]
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>PasswordHash</c> value mapped to the <c>PasswordHash</c> column.
    /// </summary>
    /// <value>
    /// The <c>PasswordHash</c> entity value.
    /// </value>
    [Column("PasswordHash", TypeName = "nvarchar(max)")]
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the <c>ResetHash</c> value mapped to the <c>ResetHash</c> column.
    /// </summary>
    /// <value>
    /// The <c>ResetHash</c> entity value.
    /// </value>
    [Column("ResetHash", TypeName = "nvarchar(max)")]
    public string? ResetHash { get; set; }

    /// <summary>
    /// Gets or sets the <c>InviteHash</c> value mapped to the <c>InviteHash</c> column.
    /// </summary>
    /// <value>
    /// The <c>InviteHash</c> entity value.
    /// </value>
    [Column("InviteHash", TypeName = "nvarchar(max)")]
    public string? InviteHash { get; set; }

    /// <summary>
    /// Gets or sets the <c>AccessFailedCount</c> value mapped to the <c>AccessFailedCount</c> column.
    /// </summary>
    /// <value>
    /// The <c>AccessFailedCount</c> entity value.
    /// </value>
    [Column("AccessFailedCount", TypeName = "int")]
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the <c>LockoutEnabled</c> value mapped to the <c>LockoutEnabled</c> column.
    /// </summary>
    /// <value>
    /// The <c>LockoutEnabled</c> entity value.
    /// </value>
    [Column("LockoutEnabled", TypeName = "bit")]
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the <c>LockoutEnd</c> value mapped to the <c>LockoutEnd</c> column.
    /// </summary>
    /// <value>
    /// The <c>LockoutEnd</c> entity value.
    /// </value>
    [Column("LockoutEnd", TypeName = "datetimeoffset")]
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets the <c>LastLogin</c> value mapped to the <c>LastLogin</c> column.
    /// </summary>
    /// <value>
    /// The <c>LastLogin</c> entity value.
    /// </value>
    [Column("LastLogin", TypeName = "datetimeoffset")]
    public DateTimeOffset? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the <c>IsDeleted</c> value mapped to the <c>IsDeleted</c> column.
    /// </summary>
    /// <value>
    /// The <c>IsDeleted</c> entity value.
    /// </value>
    [Column("IsDeleted", TypeName = "bit")]
    public bool IsDeleted { get; set; }

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
    /// Gets or sets the related <see cref="Task" /> entity collection.
    /// </summary>
    /// <value>
    /// The related <see cref="Task" /> entity collection.
    /// </value>
    public virtual ICollection<Task> AssignedTasks { get; set; }

    /// <summary>
    /// Gets or sets the related <see cref="UserLogin" /> entity collection.
    /// </summary>
    /// <value>
    /// The related <see cref="UserLogin" /> entity collection.
    /// </value>
    public virtual ICollection<UserLogin> UserLogins { get; set; }

    /// <summary>
    /// Gets or sets the related <see cref="UserRole" /> entity collection.
    /// </summary>
    /// <value>
    /// The related <see cref="UserRole" /> entity collection.
    /// </value>
    public virtual ICollection<UserRole> UserRoles { get; set; }

    #endregion

}
