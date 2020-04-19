using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Data.Tracker.Entities
{
    /// <summary>
    /// Entity class representing data for table 'Audit'.
    /// </summary>
    public partial class TrackerAudit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerAudit"/> class.
        /// </summary>
        public TrackerAudit()
        {
            #region Generated Constructor
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
        /// Gets or sets the property value representing column 'Date'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Date'.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'UserId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'UserId'.
        /// </value>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'TaskId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'TaskId'.
        /// </value>
        public Guid? TaskId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'Content'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Content'.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'Username'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Username'.
        /// </value>
        public string Username { get; set; }

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
        #endregion

    }
}
