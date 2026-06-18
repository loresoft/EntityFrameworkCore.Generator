using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// Represents a update model for the <c>Role</c> entity mapped to the <c>Role</c> table.
/// </summary>
public partial class RoleUpdateModel
    : EntityUpdateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the <c>Name</c> value mapped from the <c>Name</c> column.
    /// </summary>
    /// <value>
    /// The <c>Name</c> model value.
    /// </value>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>Description</c> value mapped from the <c>Description</c> column.
    /// </summary>
    /// <value>
    /// The <c>Description</c> model value.
    /// </value>
    public string? Description { get; set; }

    #endregion

}
