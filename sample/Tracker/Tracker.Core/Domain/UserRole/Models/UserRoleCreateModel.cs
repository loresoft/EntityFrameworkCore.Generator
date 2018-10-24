using System;
using System.Collections.Generic;

namespace Tracker.Core.Domain.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class UserRoleCreateModel
        : EntityCreateModel
    {
        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value for 'UserId'.
        /// </summary>
        /// <value>
        /// The property value for 'UserId'.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'RoleId'.
        /// </summary>
        /// <value>
        /// The property value for 'RoleId'.
        /// </value>
        public Guid RoleId { get; set; }

        #endregion

    }
}
