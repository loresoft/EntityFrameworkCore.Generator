using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class TenantCreateModel
    : EntityCreateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for <c>Name</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Name</c>.
    /// </value>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for <c>Description</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Description</c>.
    /// </value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>IsActive</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>IsActive</c>.
    /// </value>
    public bool IsActive { get; set; }

    #endregion

}
