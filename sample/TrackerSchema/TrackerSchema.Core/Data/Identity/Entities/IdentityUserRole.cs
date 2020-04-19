using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Data.Identity.Entities
{
    /// <summary>
    /// Entity class representing data for table 'UserRole'.
    /// </summary>
    public partial class IdentityUserRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserRole"/> class.
        /// </summary>
        public IdentityUserRole()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value representing column 'UserId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'UserId'.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'RoleId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'RoleId'.
        /// </value>
        public Guid RoleId { get; set; }

        #endregion

        #region Generated Relationships
        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="IdentityRole" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="IdentityRole" />.
        /// </value>
        /// <seealso cref="RoleId" />
        public virtual IdentityRole RoleIdentityRole { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="IdentityUser" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="IdentityUser" />.
        /// </value>
        /// <seealso cref="UserId" />
        public virtual IdentityUser UserIdentityUser { get; set; }

        #endregion

    }
}
