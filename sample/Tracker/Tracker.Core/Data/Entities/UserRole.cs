using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Represents the <c>UserRole</c> entity mapped to the <c>dbo.UserRole</c> table.
/// </summary>
[Table("UserRole", Schema = "dbo")]
public partial class UserRole
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRole"/> class.
    /// </summary>
    public UserRole()
    {
        #region Generated Constructor
        #endregion
    }

    #region Generated Properties
    /// <summary>
    /// Gets or sets the <c>UserId</c> value mapped to the <c>UserId</c> column.
    /// </summary>
    /// <value>
    /// The <c>UserId</c> entity value.
    /// </value>
    [Key]
    [Column("UserId", TypeName = "uniqueidentifier")]
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the <c>RoleId</c> value mapped to the <c>RoleId</c> column.
    /// </summary>
    /// <value>
    /// The <c>RoleId</c> entity value.
    /// </value>
    [Key]
    [Column("RoleId", TypeName = "uniqueidentifier")]
    public Guid RoleId { get; set; }

    #endregion

    #region Generated Relationships
    /// <summary>
    /// Gets or sets the related <see cref="Role" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="Role" /> entity.
    /// </value>
    /// <seealso cref="RoleId" />
    public virtual Role Role { get; set; } = null!;

    /// <summary>
    /// Gets or sets the related <see cref="User" /> entity.
    /// </summary>
    /// <value>
    /// The related <see cref="User" /> entity.
    /// </value>
    /// <seealso cref="UserId" />
    public virtual User User { get; set; } = null!;

    #endregion

}
