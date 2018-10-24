using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="Status"/> .
    /// </summary>
    public partial class StatusProfile
        : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusProfile"/> class.
        /// </summary>
        public StatusProfile()
        {
            CreateMap<Status, StatusReadModel>();
            CreateMap<StatusCreateModel, Status>();
            CreateMap<StatusUpdateModel, Status>();
        }

    }
}
