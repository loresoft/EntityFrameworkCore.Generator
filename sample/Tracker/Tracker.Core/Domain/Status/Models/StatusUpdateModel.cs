using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class StatusUpdateModel
        : EntityUpdateModel
    {
        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value for 'Name'.
        /// </summary>
        /// <value>
        /// The property value for 'Name'.
        /// </value>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the property value for 'Description'.
        /// </summary>
        /// <value>
        /// The property value for 'Description'.
        /// </value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'DisplayOrder'.
        /// </summary>
        /// <value>
        /// The property value for 'DisplayOrder'.
        /// </value>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'IsActive'.
        /// </summary>
        /// <value>
        /// The property value for 'IsActive'.
        /// </value>
        public bool IsActive { get; set; }

        #endregion

    }
}
