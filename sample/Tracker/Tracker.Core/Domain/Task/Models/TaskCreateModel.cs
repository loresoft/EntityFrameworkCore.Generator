using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// Represents a create model for the <c>Task</c> entity mapped to the <c>Task</c> table.
/// </summary>
public partial class TaskCreateModel
    : EntityCreateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the <c>StatusId</c> value mapped from the <c>StatusId</c> column.
    /// </summary>
    /// <value>
    /// The <c>StatusId</c> model value.
    /// </value>
    public Guid StatusId { get; set; }

    /// <summary>
    /// Gets or sets the <c>PriorityId</c> value mapped from the <c>PriorityId</c> column.
    /// </summary>
    /// <value>
    /// The <c>PriorityId</c> model value.
    /// </value>
    public Guid? PriorityId { get; set; }

    /// <summary>
    /// Gets or sets the <c>Title</c> value mapped from the <c>Title</c> column.
    /// </summary>
    /// <value>
    /// The <c>Title</c> model value.
    /// </value>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>Description</c> value mapped from the <c>Description</c> column.
    /// </summary>
    /// <value>
    /// The <c>Description</c> model value.
    /// </value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the <c>StartDate</c> value mapped from the <c>StartDate</c> column.
    /// </summary>
    /// <value>
    /// The <c>StartDate</c> model value.
    /// </value>
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the <c>DueDate</c> value mapped from the <c>DueDate</c> column.
    /// </summary>
    /// <value>
    /// The <c>DueDate</c> model value.
    /// </value>
    public DateTimeOffset? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the <c>CompleteDate</c> value mapped from the <c>CompleteDate</c> column.
    /// </summary>
    /// <value>
    /// The <c>CompleteDate</c> model value.
    /// </value>
    public DateTimeOffset? CompleteDate { get; set; }

    /// <summary>
    /// Gets or sets the <c>AssignedId</c> value mapped from the <c>AssignedId</c> column.
    /// </summary>
    /// <value>
    /// The <c>AssignedId</c> model value.
    /// </value>
    public Guid? AssignedId { get; set; }

    /// <summary>
    /// Gets or sets the <c>TenantId</c> value mapped from the <c>TenantId</c> column.
    /// </summary>
    /// <value>
    /// The <c>TenantId</c> model value.
    /// </value>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the <c>PeriodStart1</c> value mapped from the <c>PeriodStart</c> column.
    /// </summary>
    /// <value>
    /// The <c>PeriodStart1</c> model value.
    /// </value>
    public DateTime PeriodStart1 { get; set; }

    /// <summary>
    /// Gets or sets the <c>PeriodEnd1</c> value mapped from the <c>PeriodEnd</c> column.
    /// </summary>
    /// <value>
    /// The <c>PeriodEnd1</c> model value.
    /// </value>
    public DateTime PeriodEnd1 { get; set; }

    #endregion

}
