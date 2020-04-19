using System;
using System.Collections.Generic;

namespace Tracker.PostgreSQL.Core.Data.Entities
{
    public partial class Task
    {
        public Task()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public Guid Id { get; set; }

        public Guid StatusId { get; set; }

        public Guid? PriorityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public Guid? AssignedId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        #endregion

        #region Generated Relationships
        public virtual User AssignedUser { get; set; }

        public virtual Priority Priority { get; set; }

        public virtual Status Status { get; set; }

        public virtual TaskExtended TaskExtended { get; set; }

        #endregion

    }
}
