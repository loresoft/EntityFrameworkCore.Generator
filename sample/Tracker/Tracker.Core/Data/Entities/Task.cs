using System;
using System.Collections.Generic;

namespace Tracker.Core.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'Task'.
    /// </summary>
    public partial class Task
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class.
        /// </summary>
        public Task()
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
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the property value representing column 'PriorityId'.
        /// </summary>
        /// <value>
        /// The property value representing column 'PriorityId'.
        /// </value>
        public int? PriorityId { get; set; }

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
        /// Gets or sets the navigation property for entity <see cref="Priority" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="Priority" />.
        /// </value>
        /// <seealso cref="PriorityId" />
        public virtual Priority Priority { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="Status" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="Status" />.
        /// </value>
        /// <seealso cref="StatusId" />
        public virtual Status Status { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="User" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="User" />.
        /// </value>
        /// <seealso cref="AssignedId" />
        public virtual User AssignedUser { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for entity <see cref="TaskExtended" />.
        /// </summary>
        /// <value>
        /// The the navigation property for entity <see cref="TaskExtended" />.
        /// </value>
        /// <seealso cref="Id" />
        public virtual TaskExtended TaskExtended { get; set; }

        #endregion

    }
}
