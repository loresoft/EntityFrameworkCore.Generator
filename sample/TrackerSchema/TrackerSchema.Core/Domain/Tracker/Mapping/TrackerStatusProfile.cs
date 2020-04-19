using System;
using AutoMapper;
using TrackerSchema.Core.Data.Tracker.Entities;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="TrackerStatus"/> .
    /// </summary>
    public partial class TrackerStatusProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerStatusProfile"/> class.
        /// </summary>
        public TrackerStatusProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus, TrackerSchema.Core.Domain.Tracker.Models.TrackerStatusReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerStatusCreateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus>();

            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus, TrackerSchema.Core.Domain.Tracker.Models.TrackerStatusUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerStatusUpdateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerStatus>();

        }

    }
}
