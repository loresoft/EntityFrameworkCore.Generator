using System;
using System.Collections.Generic;

namespace Tracker.Domain.Models
{
    public partial class AuditCreateModel
    {
        #region Generated Properties
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid? UserId { get; set; }

        public Guid? TaskId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        #endregion

    }
}
