using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// Represents a create model for the <c>Priority</c> entity mapped to the <c>Priority</c> table.
/// </summary>
public partial class PriorityCreateModel
    : EntityCreateModel
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

    /// <summary>
    /// Gets or sets the <c>DisplayOrder</c> value mapped from the <c>DisplayOrder</c> column.
    /// </summary>
    /// <value>
    /// The <c>DisplayOrder</c> model value.
    /// </value>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Gets or sets the <c>IsActive</c> value mapped from the <c>IsActive</c> column.
    /// </summary>
    /// <value>
    /// The <c>IsActive</c> model value.
    /// </value>
    public bool IsActive { get; set; }

    #endregion

}
