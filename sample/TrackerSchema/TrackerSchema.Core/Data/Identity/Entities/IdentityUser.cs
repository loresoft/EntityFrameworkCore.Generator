using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Data.Identity.Entities
{
    /// <summary>
    /// Entity class representing data for table 'User'.
    /// </summary>
    public partial class IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUser"/> class.
        /// </summary>
        public IdentityUser()
        {
            #region Generated Constructor
            AssignedTrackerTasks = new HashSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask>();
            UserIdentityUserLogins = new HashSet<IdentityUserLogin>();
            UserIdentityUserRoles = new HashSet<IdentityUserRole>();
            #endregion
        }

        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value representing column 'Id'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Id'.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'EmailAddress'.
        /// </summary>
        /// <value>
        /// The property value representing column 'EmailAddress'.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'IsEmailAddressConfirmed'.
        /// </summary>
        /// <value>
        /// The property value representing column 'IsEmailAddressConfirmed'.
        /// </value>
        public bool IsEmailAddressConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'DisplayName'.
        /// </summary>
        /// <value>
        /// The property value representing column 'DisplayName'.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'PasswordHash'.
        /// </summary>
        /// <value>
        /// The property value representing column 'PasswordHash'.
        /// </value>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'ResetHash'.
        /// </summary>
        /// <value>
        /// The property value representing column 'ResetHash'.
        /// </value>
        public string ResetHash { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'InviteHash'.
        /// </summary>
        /// <value>
        /// The property value representing column 'InviteHash'.
        /// </value>
        public string InviteHash { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'AccessFailedCount'.
        /// </summary>
        /// <value>
        /// The property value representing column 'AccessFailedCount'.
        /// </value>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'LockoutEnabled'.
        /// </summary>
        /// <value>
        /// The property value representing column 'LockoutEnabled'.
        /// </value>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'LockoutEnd'.
        /// </summary>
        /// <value>
        /// The property value representing column 'LockoutEnd'.
        /// </value>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'LastLogin'.
        /// </summary>
        /// <value>
        /// The property value representing column 'LastLogin'.
        /// </value>
        public DateTimeOffset? LastLogin { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'IsDeleted'.
        /// </summary>
        /// <value>
        /// The property value representing column 'IsDeleted'.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'Created'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Created'.
        /// </value>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'CreatedBy'.
        /// </summary>
        /// <value>
        /// The property value representing column 'CreatedBy'.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'Updated'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Updated'.
        /// </value>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'UpdatedBy'.
        /// </summary>
        /// <value>
        /// The property value representing column 'UpdatedBy'.
        /// </value>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'RowVersion'.
        /// </summary>
        /// <value>
        /// The property value representing column 'RowVersion'.
        /// </value>
        public Byte[] RowVersion { get; set; }

        #endregion

        #region Generated Relationships
        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTask" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTask" />.
        /// </value>
        public virtual ICollection<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> AssignedTrackerTasks { get; set; }

        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="IdentityUserLogin" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="IdentityUserLogin" />.
        /// </value>
        public virtual ICollection<IdentityUserLogin> UserIdentityUserLogins { get; set; }

        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="IdentityUserRole" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="IdentityUserRole" />.
        /// </value>
        public virtual ICollection<IdentityUserRole> UserIdentityUserRoles { get; set; }

        #endregion

    }
}
