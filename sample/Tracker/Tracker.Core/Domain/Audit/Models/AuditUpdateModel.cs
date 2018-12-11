using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class AuditUpdateModel
        : EntityUpdateModel
    {
        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value for 'Date'.
        /// </summary>
        /// <value>
        /// The property value for 'Date'.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'UserId'.
        /// </summary>
        /// <value>
        /// The property value for 'UserId'.
        /// </value>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'TaskId'.
        /// </summary>
        /// <value>
        /// The property value for 'TaskId'.
        /// </value>
        public Guid? TaskId { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Content'.
        /// </summary>
        /// <value>
        /// The property value for 'Content'.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Username'.
        /// </summary>
        /// <value>
        /// The property value for 'Username'.
        /// </value>
        public string Username { get; set; }

        #endregion

    }
}
