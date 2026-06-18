using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class UserRoleCreateModel
    : EntityCreateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for <c>UserId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>UserId</c>.
    /// </value>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>RoleId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>RoleId</c>.
    /// </value>
    public Guid RoleId { get; set; }

    #endregion

}
