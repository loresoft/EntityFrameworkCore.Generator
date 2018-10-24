using System;
using System.Collections.Generic;

namespace Tracker.Core.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'User'.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            #region Generated Constructor
            AssignedTasks = new HashSet<Task>();
            UserLogins = new HashSet<UserLogin>();
            UserRoles = new HashSet<UserRole>();
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
        /// Gets or sets the navigation collection for entity <see cref="Task" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="Task" />.
        /// </value>
        public virtual ICollection<Task> AssignedTasks { get; set; }

        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="UserLogin" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="UserLogin" />.
        /// </value>
        public virtual ICollection<UserLogin> UserLogins { get; set; }

        /// <summary>
        /// Gets or sets the navigation collection for entity <see cref="UserRole" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="UserRole" />.
        /// </value>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        #endregion

    }
}
