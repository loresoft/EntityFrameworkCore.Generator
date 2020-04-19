using System;
using AutoMapper;
using TrackerSchema.Core.Data.Tracker.Entities;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="TrackerAudit"/> .
    /// </summary>
    public partial class TrackerAuditProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerAuditProfile"/> class.
        /// </summary>
        public TrackerAuditProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit, TrackerSchema.Core.Domain.Tracker.Models.TrackerAuditReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerAuditCreateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit>();

            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit, TrackerSchema.Core.Domain.Tracker.Models.TrackerAuditUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerAuditUpdateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerAudit>();

        }

    }
}
