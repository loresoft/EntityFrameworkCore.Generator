using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models;

/// <summary>
/// Represents a update model for the <c>User</c> entity mapped to the <c>User</c> table.
/// </summary>
public partial class UserUpdateModel
    : EntityUpdateModel
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
    /// Gets or sets the <c>IsEmailAddressConfirmed</c> value mapped from the <c>IsEmailAddressConfirmed</c> column.
    /// </summary>
    /// <value>
    /// The <c>IsEmailAddressConfirmed</c> model value.
    /// </value>
    public bool IsEmailAddressConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the <c>DisplayName</c> value mapped from the <c>DisplayName</c> column.
    /// </summary>
    /// <value>
    /// The <c>DisplayName</c> model value.
    /// </value>
    public string DisplayName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <c>AccessFailedCount</c> value mapped from the <c>AccessFailedCount</c> column.
    /// </summary>
    /// <value>
    /// The <c>AccessFailedCount</c> model value.
    /// </value>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the <c>LockoutEnabled</c> value mapped from the <c>LockoutEnabled</c> column.
    /// </summary>
    /// <value>
    /// The <c>LockoutEnabled</c> model value.
    /// </value>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the <c>LockoutEnd</c> value mapped from the <c>LockoutEnd</c> column.
    /// </summary>
    /// <value>
    /// The <c>LockoutEnd</c> model value.
    /// </value>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets the <c>LastLogin</c> value mapped from the <c>LastLogin</c> column.
    /// </summary>
    /// <value>
    /// The <c>LastLogin</c> model value.
    /// </value>
    public DateTimeOffset? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the <c>IsDeleted</c> value mapped from the <c>IsDeleted</c> column.
    /// </summary>
    /// <value>
    /// The <c>IsDeleted</c> model value.
    /// </value>
    public bool IsDeleted { get; set; }

    #endregion

}
