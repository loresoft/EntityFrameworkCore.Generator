using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class TaskExtendedReadModel
    : EntityReadModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for <c>TaskId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>TaskId</c>.
    /// </value>
    public Guid TaskId { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>UserAgent</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>UserAgent</c>.
    /// </value>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>Browser</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Browser</c>.
    /// </value>
    public string? Browser { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>OperatingSystem</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>OperatingSystem</c>.
    /// </value>
    public string? OperatingSystem { get; set; }

    #endregion

}
