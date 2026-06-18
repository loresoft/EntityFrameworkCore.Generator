using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class TaskUpdateModel
    : EntityUpdateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for <c>StatusId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>StatusId</c>.
    /// </value>
    public Guid StatusId { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>PriorityId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>PriorityId</c>.
    /// </value>
    public Guid? PriorityId { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>Title</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Title</c>.
    /// </value>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for <c>Description</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>Description</c>.
    /// </value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>StartDate</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>StartDate</c>.
    /// </value>
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>DueDate</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>DueDate</c>.
    /// </value>
    public DateTimeOffset? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>CompleteDate</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>CompleteDate</c>.
    /// </value>
    public DateTimeOffset? CompleteDate { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>AssignedId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>AssignedId</c>.
    /// </value>
    public Guid? AssignedId { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>TenantId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>TenantId</c>.
    /// </value>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>PeriodStart1</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>PeriodStart1</c>.
    /// </value>
    public DateTime PeriodStart1 { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>PeriodEnd1</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>PeriodEnd1</c>.
    /// </value>
    public DateTime PeriodEnd1 { get; set; }

    #endregion

}
