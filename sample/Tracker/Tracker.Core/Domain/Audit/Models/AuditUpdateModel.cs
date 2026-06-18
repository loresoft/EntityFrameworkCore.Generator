using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class AuditUpdateModel
    : EntityUpdateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for <c>Date</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Date</c>.
    /// </value>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>UserId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>UserId</c>.
    /// </value>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>TaskId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>TaskId</c>.
    /// </value>
    public Guid? TaskId { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>Content</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Content</c>.
    /// </value>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for <c>Username</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Username</c>.
    /// </value>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for <c>Attributes</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Attributes</c>.
    /// </value>
    public string? Attributes { get; set; }

    #endregion

}
