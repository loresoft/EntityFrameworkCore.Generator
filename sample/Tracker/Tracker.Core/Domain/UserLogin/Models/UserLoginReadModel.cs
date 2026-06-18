using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class UserLoginReadModel
    : EntityReadModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for <c>EmailAddress</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>EmailAddress</c>.
    /// </value>
    public string EmailAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for <c>UserId</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>UserId</c>.
    /// </value>
    public Guid? UserId { get; set; }

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

    /// <summary>
    /// Gets or sets the property value for <c>DeviceFamily</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>DeviceFamily</c>.
    /// </value>
    public string? DeviceFamily { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>DeviceBrand</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>DeviceBrand</c>.
    /// </value>
    public string? DeviceBrand { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>DeviceModel</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>DeviceModel</c>.
    /// </value>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>IpAddress</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>IpAddress</c>.
    /// </value>
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>IsSuccessful</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>IsSuccessful</c>.
    /// </value>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>FailureMessage</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>FailureMessage</c>.
    /// </value>
    public string? FailureMessage { get; set; }

    #endregion

}
