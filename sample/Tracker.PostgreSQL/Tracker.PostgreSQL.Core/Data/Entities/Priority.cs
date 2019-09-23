using System;
using System.Collections.Generic;

namespace Tracker.Data.Entities
{
    public partial class Priority
    {
        public Priority()
        {
            #region Generated Constructor
            Tasks = new HashSet<Task>();
            #endregion
        }

        #region Generated Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Task> Tasks { get; set; }

        #endregion

    }
}
