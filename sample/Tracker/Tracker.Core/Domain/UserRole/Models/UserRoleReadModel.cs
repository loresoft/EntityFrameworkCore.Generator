using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// Represents a read model for the <c>UserRole</c> entity mapped to the <c>UserRole</c> table.
/// </summary>
public partial class UserRoleReadModel
    : EntityReadModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the <c>UserId</c> value mapped from the <c>UserId</c> column.
    /// </summary>
    /// <value>
    /// The <c>UserId</c> model value.
    /// </value>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the <c>RoleId</c> value mapped from the <c>RoleId</c> column.
    /// </summary>
    /// <value>
    /// The <c>RoleId</c> model value.
    /// </value>
    public Guid RoleId { get; set; }

    #endregion

}
