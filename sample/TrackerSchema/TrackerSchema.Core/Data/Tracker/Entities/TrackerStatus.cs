using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Data.Tracker.Entities
{
    /// <summary>
    /// Entity class representing data for table 'Status'.
    /// </summary>
    public partial class TrackerStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerStatus"/> class.
        /// </summary>
        public TrackerStatus()
        {
            #region Generated Constructor
            StatusTrackerTasks = new HashSet<TrackerTask>();
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
        /// Gets or sets the property value representing column 'Name'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Name'.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'Description'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Description'.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'DisplayOrder'.
        /// </summary>
        /// <value>
        /// The property value representing column 'DisplayOrder'.
        /// </value>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'IsActive'.
        /// </summary>
        /// <value>
        /// The property value representing column 'IsActive'.
        /// </value>
        public bool IsActive { get; set; }

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
        /// Gets or sets the navigation collection for entity <see cref="TrackerTask" />.
        /// </summary>
        /// <value>
        /// The the navigation collection for entity <see cref="TrackerTask" />.
        /// </value>
        public virtual ICollection<TrackerTask> StatusTrackerTasks { get; set; }

        #endregion

    }
}
