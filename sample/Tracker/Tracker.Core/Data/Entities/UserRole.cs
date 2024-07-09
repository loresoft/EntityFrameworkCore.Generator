using System;
using System.Collections.Generic;

namespace Tracker.Core.Data.Entities;

/// <summary>
/// Entity class representing data for table 'UserRole'.
/// </summary>
[System.ComponentModel.DataAnnotations.Schema.Table("UserRole", Schema = "dbo")]
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
    /// Gets or sets the property value representing column 'UserId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'UserId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Key()]
    [System.ComponentModel.DataAnnotations.Schema.Column("UserId", TypeName = "uniqueidentifier")]
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the property value representing column 'RoleId'.
    /// </summary>
    /// <value>
    /// The property value representing column 'RoleId'.
    /// </value>
    [System.ComponentModel.DataAnnotations.Key()]
    [System.ComponentModel.DataAnnotations.Schema.Column("RoleId", TypeName = "uniqueidentifier")]
    public Guid RoleId { get; set; }

    #endregion

    #region Generated Relationships
    /// <summary>
    /// Gets or sets the navigation property for entity <see cref="Role" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="Role" />.
    /// </value>
    /// <seealso cref="RoleId" />
    public virtual Role Role { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation property for entity <see cref="User" />.
    /// </summary>
    /// <value>
    /// The navigation property for entity <see cref="User" />.
    /// </value>
    /// <seealso cref="UserId" />
    public virtual User User { get; set; } = null!;

    #endregion

}
