using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// Represents a read model for the <c>Audit</c> entity mapped to the <c>Audit</c> table.
/// </summary>
public partial class AuditReadModel
    : EntityReadModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the <c>Date</c> value mapped from the <c>Date</c> column.
    /// </summary>
    /// <value>
    /// The <c>Date</c> model value.
    /// </value>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the <c>UserId</c> value mapped from the <c>UserId</c> column.
    /// </summary>
    /// <value>
    /// The <c>UserId</c> model value.
    /// </value>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the <c>TaskId</c> value mapped from the <c>TaskId</c> column.
    /// </summary>
    /// <value>
    /// The <c>TaskId</c> model value.
    /// </value>
    public Guid? TaskId { get; set; }

    /// <summary>
    /// Gets or sets the <c>Content</c> value mapped from the <c>Content</c> column.
    /// </summary>
    /// <value>
    /// The <c>Content</c> model value.
    /// </value>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>Username</c> value mapped from the <c>Username</c> column.
    /// </summary>
    /// <value>
    /// The <c>Username</c> model value.
    /// </value>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>Attributes</c> value mapped from the <c>Attributes</c> column.
    /// </summary>
    /// <value>
    /// The <c>Attributes</c> model value.
    /// </value>
    public string? Attributes { get; set; }

    #endregion

}
