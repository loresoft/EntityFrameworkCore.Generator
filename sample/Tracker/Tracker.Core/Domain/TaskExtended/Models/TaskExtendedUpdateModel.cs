using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class TaskExtendedUpdateModel
    : EntityUpdateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for 'TaskId'.
    /// </summary>
    /// <value>
    /// The property value for 'TaskId'.
    /// </value>
    public Guid TaskId { get; set; }

    /// <summary>
    /// Gets or sets the property value for 'UserAgent'.
    /// </summary>
    /// <value>
    /// The property value for 'UserAgent'.
    /// </value>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the property value for 'Browser'.
    /// </summary>
    /// <value>
    /// The property value for 'Browser'.
    /// </value>
    public string? Browser { get; set; }

    /// <summary>
    /// Gets or sets the property value for 'OperatingSystem'.
    /// </summary>
    /// <value>
    /// The property value for 'OperatingSystem'.
    /// </value>
    public string? OperatingSystem { get; set; }

    #endregion

}
