using System;
using System.Collections.Generic;

namespace Tracker.PostgreSQL.Core.Domain.Models
{
    public partial class UserLoginUpdateModel
    {
        #region Generated Properties
        public Guid Id { get; set; }

        public string EmailAddress { get; set; }

        public Guid? UserId { get; set; }

        public string UserAgent { get; set; }

        public string Browser { get; set; }

        public string OperatingSystem { get; set; }

        public string DeviceFamily { get; set; }

        public string DeviceBrand { get; set; }

        public string DeviceModel { get; set; }

        public string IpAddress { get; set; }

        public bool IsSuccessful { get; set; }

        public string FailureMessage { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        #endregion

    }
}
