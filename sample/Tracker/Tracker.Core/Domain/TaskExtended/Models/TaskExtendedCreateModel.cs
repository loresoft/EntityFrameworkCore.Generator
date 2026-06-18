using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// Represents a create model for the <c>TaskExtended</c> entity mapped to the <c>TaskExtended</c> table.
/// </summary>
public partial class TaskExtendedCreateModel
    : EntityCreateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the <c>TaskId</c> value mapped from the <c>TaskId</c> column.
    /// </summary>
    /// <value>
    /// The <c>TaskId</c> model value.
    /// </value>
    public Guid TaskId { get; set; }

    /// <summary>
    /// Gets or sets the <c>UserAgent</c> value mapped from the <c>UserAgent</c> column.
    /// </summary>
    /// <value>
    /// The <c>UserAgent</c> model value.
    /// </value>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the <c>Browser</c> value mapped from the <c>Browser</c> column.
    /// </summary>
    /// <value>
    /// The <c>Browser</c> model value.
    /// </value>
    public string? Browser { get; set; }

    /// <summary>
    /// Gets or sets the <c>OperatingSystem</c> value mapped from the <c>OperatingSystem</c> column.
    /// </summary>
    /// <value>
    /// The <c>OperatingSystem</c> model value.
    /// </value>
    public string? OperatingSystem { get; set; }

    #endregion

}
