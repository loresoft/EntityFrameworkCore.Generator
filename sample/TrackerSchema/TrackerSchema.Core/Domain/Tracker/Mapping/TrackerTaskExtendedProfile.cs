using System;
using AutoMapper;
using TrackerSchema.Core.Data.Tracker.Entities;
using TrackerSchema.Core.Domain.Tracker.Models;

namespace TrackerSchema.Core.Domain.Tracker.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="TrackerTaskExtended"/> .
    /// </summary>
    public partial class TrackerTaskExtendedProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerTaskExtendedProfile"/> class.
        /// </summary>
        public TrackerTaskExtendedProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended, TrackerSchema.Core.Domain.Tracker.Models.TrackerTaskExtendedReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerTaskExtendedCreateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended>();

            CreateMap<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended, TrackerSchema.Core.Domain.Tracker.Models.TrackerTaskExtendedUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Tracker.Models.TrackerTaskExtendedUpdateModel, TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended>();

        }

    }
}
