using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Domain.Tracker.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class TrackerAuditCreateModel
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
