using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class RoleCreateModel
    : EntityCreateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for 'Name'.
    /// </summary>
    /// <value>
    /// The property value for 'Name'.
    /// </value>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for 'Description'.
    /// </summary>
    /// <value>
    /// The property value for 'Description'.
    /// </value>
    public string? Description { get; set; }

    #endregion

}
