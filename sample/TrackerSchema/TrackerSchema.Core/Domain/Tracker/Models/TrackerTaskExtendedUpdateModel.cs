using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Domain.Tracker.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class TrackerTaskExtendedUpdateModel
    {
        #region Generated Properties
        /// <summary>
        /// Gets or sets the property value for 'TaskId'.
        /// </summary>
        /// <value>
        /// The property value for 'TaskId'.
        /// </value>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'UserAgent'.
        /// </summary>
        /// <value>
        /// The property value for 'UserAgent'.
        /// </value>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Browser'.
        /// </summary>
        /// <value>
        /// The property value for 'Browser'.
        /// </value>
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'OperatingSystem'.
        /// </summary>
        /// <value>
        /// The property value for 'OperatingSystem'.
        /// </value>
        public string OperatingSystem { get; set; }

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
