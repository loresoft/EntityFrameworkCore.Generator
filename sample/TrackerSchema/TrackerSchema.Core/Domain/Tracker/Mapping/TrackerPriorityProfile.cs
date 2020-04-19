using System;
using AutoMapper;
using TrackerSchema.Core.Data.Tracker.Entities;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="TrackerPriority"/> .
    /// </summary>
    public partial class TrackerPriorityProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerPriorityProfile"/> class.
        /// </summary>
        public TrackerPriorityProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority, TrackerSchema.Core.Domain.Tracker.Models.TrackerPriorityReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerPriorityCreateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority>();

            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority, TrackerSchema.Core.Domain.Tracker.Models.TrackerPriorityUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerPriorityUpdateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority>();

        }

    }
}
