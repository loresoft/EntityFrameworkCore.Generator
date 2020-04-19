using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Domain.Identity.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class IdentityUserUpdateModel
    {
        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value for 'Id'.
        /// </summary>
        /// <value>
        /// The property value for 'Id'.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'EmailAddress'.
        /// </summary>
        /// <value>
        /// The property value for 'EmailAddress'.
        /// </value>
        public string EmailAddress { get; set; }

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
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'PasswordHash'.
        /// </summary>
        /// <value>
        /// The property value for 'PasswordHash'.
        /// </value>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'ResetHash'.
        /// </summary>
        /// <value>
        /// The property value for 'ResetHash'.
        /// </value>
        public string ResetHash { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'InviteHash'.
        /// </summary>
        /// <value>
        /// The property value for 'InviteHash'.
        /// </value>
        public string InviteHash { get; set; }

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

        /// <summary>
        /// Gets or sets the property value for 'Created'.
        /// </summary>
        /// <value>
        /// The property value for 'Created'.
        /// </value>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'CreatedBy'.
        /// </summary>
        /// <value>
        /// The property value for 'CreatedBy'.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Updated'.
        /// </summary>
        /// <value>
        /// The property value for 'Updated'.
        /// </value>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'UpdatedBy'.
        /// </summary>
        /// <value>
        /// The property value for 'UpdatedBy'.
        /// </value>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'RowVersion'.
        /// </summary>
        /// <value>
        /// The property value for 'RowVersion'.
        /// </value>
        public Byte[] RowVersion { get; set; }

        #endregion

    }
}
