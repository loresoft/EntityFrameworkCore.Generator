using System;
using System.Collections.Generic;

namespace TrackerSchema.Core.Domain.Identity.Models
{
    /// <summary>
    /// View Model class
    /// </summary>
    public partial class IdentityUserLoginReadModel
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
        /// Gets or sets the property value for 'EmailAddress'.
        /// </summary>
        /// <value>
        /// The property value for 'EmailAddress'.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'UserId'.
        /// </summary>
        /// <value>
        /// The property value for 'UserId'.
        /// </value>
        public Guid? UserId { get; set; }

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
        /// Gets or sets the property value for 'DeviceFamily'.
        /// </summary>
        /// <value>
        /// The property value for 'DeviceFamily'.
        /// </value>
        public string DeviceFamily { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'DeviceBrand'.
        /// </summary>
        /// <value>
        /// The property value for 'DeviceBrand'.
        /// </value>
        public string DeviceBrand { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'DeviceModel'.
        /// </summary>
        /// <value>
        /// The property value for 'DeviceModel'.
        /// </value>
        public string DeviceModel { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'IpAddress'.
        /// </summary>
        /// <value>
        /// The property value for 'IpAddress'.
        /// </value>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'IsSuccessful'.
        /// </summary>
        /// <value>
        /// The property value for 'IsSuccessful'.
        /// </value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the property value for 'FailureMessage'.
        /// </summary>
        /// <value>
        /// The property value for 'FailureMessage'.
        /// </value>
        public string FailureMessage { get; set; }

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
