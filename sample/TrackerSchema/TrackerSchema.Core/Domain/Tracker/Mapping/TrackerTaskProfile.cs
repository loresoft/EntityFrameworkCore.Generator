using System;
using AutoMapper;
using TrackerSchema.Core.Data.Tracker.Entities;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="TrackerTask"/> .
    /// </summary>
    public partial class TrackerTaskProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerTaskProfile"/> class.
        /// </summary>
        public TrackerTaskProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask, TrackerSchema.Core.Domain.Tracker.Models.TrackerTaskReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerTaskCreateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerTask>();

            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask, TrackerSchema.Core.Domain.Tracker.Models.TrackerTaskUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerTaskUpdateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerTask>();

        }

    }
}
