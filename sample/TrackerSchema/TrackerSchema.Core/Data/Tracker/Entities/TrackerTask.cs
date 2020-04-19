using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Data.Tracker.Entities
{
    /// <summary>
    /// Entity class representing data for table 'Task'.
    /// </summary>
    public partial class TrackerTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerTask"/> class.
        /// </summary>
        public TrackerTask()
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
        /// Gets or sets the property value representing column 'StatusId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'StatusId'.
        /// </value>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'PriorityId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'PriorityId'.
        /// </value>
        public Guid? PriorityId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'Title'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Title'.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'Description'.
        /// </summary>
        /// <value>
        /// The property value representing column 'Description'.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'StartDate'.
        /// </summary>
        /// <value>
        /// The property value representing column 'StartDate'.
        /// </value>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'DueDate'.
        /// </summary>
        /// <value>
        /// The property value representing column 'DueDate'.
        /// </value>
        public DateTimeOffset? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'CompleteDate'.
        /// </summary>
        /// <value>
        /// The property value representing column 'CompleteDate'.
        /// </value>
        public DateTimeOffset? CompleteDate { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'AssignedId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'AssignedId'.
        /// </value>
        public Guid? AssignedId { get; set; }

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
        /// Gets or sets the navigation property for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser" />.
        /// </value>
        /// <seealso cref="AssignedId" />
        public virtual TrackerSchema.Core.Data.Identity.Entities.IdentityUser AssignedIdentityUser { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="TrackerPriority" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="TrackerPriority" />.
        /// </value>
        /// <seealso cref="PriorityId" />
        public virtual TrackerPriority PriorityTrackerPriority { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="TrackerStatus" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="TrackerStatus" />.
        /// </value>
        /// <seealso cref="StatusId" />
        public virtual TrackerStatus StatusTrackerStatus { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="TrackerTaskExtended" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="TrackerTaskExtended" />.
        /// </value>
        /// <seealso cref="Id" />
        public virtual TrackerTaskExtended TaskTrackerTaskExtended { get; set; }

        #endregion

    }
}
