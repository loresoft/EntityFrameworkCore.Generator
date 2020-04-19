using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Domain.Tracker.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class TrackerTaskReadModel
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
        /// Gets or sets the property value for 'StatusId'.
        /// </summary>
        /// <value>
        /// The property value for 'StatusId'.
        /// </value>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'PriorityId'.
        /// </summary>
        /// <value>
        /// The property value for 'PriorityId'.
        /// </value>
        public Guid? PriorityId { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Title'.
        /// </summary>
        /// <value>
        /// The property value for 'Title'.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'Description'.
        /// </summary>
        /// <value>
        /// The property value for 'Description'.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'StartDate'.
        /// </summary>
        /// <value>
        /// The property value for 'StartDate'.
        /// </value>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'DueDate'.
        /// </summary>
        /// <value>
        /// The property value for 'DueDate'.
        /// </value>
        public DateTimeOffset? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'CompleteDate'.
        /// </summary>
        /// <value>
        /// The property value for 'CompleteDate'.
        /// </value>
        public DateTimeOffset? CompleteDate { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'AssignedId'.
        /// </summary>
        /// <value>
        /// The property value for 'AssignedId'.
        /// </value>
        public Guid? AssignedId { get; set; }

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
