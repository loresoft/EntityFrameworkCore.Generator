using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Domain.Identity.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class IdentityRoleUpdateModel
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
        /// Gets or sets the property value for 'Name'.
        /// </summary>
        /// <value>
        /// The property value for 'Name'.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Description'.
        /// </summary>
        /// <value>
        /// The property value for 'Description'.
        /// </value>
        public string Description { get; set; }

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
