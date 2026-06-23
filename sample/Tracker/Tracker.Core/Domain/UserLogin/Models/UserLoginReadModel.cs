using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// Represents a read model for the <c>UserLogin</c> entity mapped to the <c>UserLogin</c> table.
/// </summary>
public partial class UserLoginReadModel
    : EntityReadModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the <c>EmailAddress</c> value mapped from the <c>EmailAddress</c> column.
    /// </summary>
    /// <value>
    /// The <c>EmailAddress</c> model value.
    /// </value>
    public string EmailAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>UserId</c> value mapped from the <c>UserId</c> column.
    /// </summary>
    /// <value>
    /// The <c>UserId</c> model value.
    /// </value>
    public Guid? UserId { get; set; }

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

    /// <summary>
    /// Gets or sets the <c>DeviceFamily</c> value mapped from the <c>DeviceFamily</c> column.
    /// </summary>
    /// <value>
    /// The <c>DeviceFamily</c> model value.
    /// </value>
    public string? DeviceFamily { get; set; }

    /// <summary>
    /// Gets or sets the <c>DeviceBrand</c> value mapped from the <c>DeviceBrand</c> column.
    /// </summary>
    /// <value>
    /// The <c>DeviceBrand</c> model value.
    /// </value>
    public string? DeviceBrand { get; set; }

    /// <summary>
    /// Gets or sets the <c>DeviceModel</c> value mapped from the <c>DeviceModel</c> column.
    /// </summary>
    /// <value>
    /// The <c>DeviceModel</c> model value.
    /// </value>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// Gets or sets the <c>IpAddress</c> value mapped from the <c>IpAddress</c> column.
    /// </summary>
    /// <value>
    /// The <c>IpAddress</c> model value.
    /// </value>
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the <c>IsSuccessful</c> value mapped from the <c>IsSuccessful</c> column.
    /// </summary>
    /// <value>
    /// The <c>IsSuccessful</c> model value.
    /// </value>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the <c>FailureMessage</c> value mapped from the <c>FailureMessage</c> column.
    /// </summary>
    /// <value>
    /// The <c>FailureMessage</c> model value.
    /// </value>
    public string? FailureMessage { get; set; }

    #endregion

}
