using System;
using System.Collections.Generic;
using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'TaskExtended'.
    /// </summary>
    public partial class TaskExtended : ITrackCreated, ITrackUpdated
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskExtended"/> class.
        /// </summary>
        public TaskExtended()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value representing column 'TaskId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'TaskId'.
        /// </value>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'UserAgent'.
        /// </summary>
        /// <value>
        /// The property value representing column 'UserAgent'.
        /// </value>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'Browser'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Browser'.
        /// </value>
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'OperatingSystem'.
        /// </summary>
        /// <value>
        /// The property value representing column 'OperatingSystem'.
        /// </value>
        public string OperatingSystem { get; set; }

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
        /// Gets or sets the navigation property for entity <see cref="Task" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="Task" />.
        /// </value>
        /// <seealso cref="TaskId" />
        public virtual Task Task { get; set; }

        #endregion

    }
}
