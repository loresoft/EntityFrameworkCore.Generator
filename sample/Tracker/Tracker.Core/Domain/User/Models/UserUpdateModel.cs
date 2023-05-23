using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models;

/// <summary>
/// View Model class
/// </summary>
public partial class UserUpdateModel
    : EntityUpdateModel
{
    #region Generated Properties
    /// <summary>
    /// Gets or sets the property value for 'EmailAddress'.
    /// </summary>
    /// <value>
    /// The property value for 'EmailAddress'.
    /// </value>
    public string EmailAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for 'IsEmailAddressConfirmed'.
    /// </summary>
    /// <value>
    /// The property value for 'IsEmailAddressConfirmed'.
    /// </value>
    public bool IsEmailAddressConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the property value for 'DisplayName'.
    /// </summary>
    /// <value>
    /// The property value for 'DisplayName'.
    /// </value>
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the property value for 'AccessFailedCount'.
    /// </summary>
    /// <value>
    /// The property value for 'AccessFailedCount'.
    /// </value>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the property value for 'LockoutEnabled'.
    /// </summary>
    /// <value>
    /// The property value for 'LockoutEnabled'.
    /// </value>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the property value for 'LockoutEnd'.
    /// </summary>
    /// <value>
    /// The property value for 'LockoutEnd'.
    /// </value>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets the property value for 'LastLogin'.
    /// </summary>
    /// <value>
    /// The property value for 'LastLogin'.
    /// </value>
    public DateTimeOffset? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the property value for 'IsDeleted'.
    /// </summary>
    /// <value>
    /// The property value for 'IsDeleted'.
    /// </value>
    public bool IsDeleted { get; set; }

    #endregion

}
