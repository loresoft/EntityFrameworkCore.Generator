using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class UserCreateModel
    : EntityCreateModel
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
    /// Gets or sets the property value for <c>IsEmailAddressConfirmed</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>IsEmailAddressConfirmed</c>.
    /// </value>
    public bool IsEmailAddressConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>DisplayName</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>DisplayName</c>.
    /// </value>
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for <c>AccessFailedCount</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>AccessFailedCount</c>.
    /// </value>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>LockoutEnabled</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>LockoutEnabled</c>.
    /// </value>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>LockoutEnd</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>LockoutEnd</c>.
    /// </value>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>LastLogin</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>LastLogin</c>.
    /// </value>
    public DateTimeOffset? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the property value for <c>IsDeleted</c>.
    /// </summary>
    /// <value>
    /// The property value for <c>IsDeleted</c>.
    /// </value>
    public bool IsDeleted { get; set; }

    #endregion

}
