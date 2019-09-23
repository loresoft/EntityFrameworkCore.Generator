using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class StatusProfile
        : AutoMapper.Profile
    {
        public StatusProfile()
        {
            CreateMap<Tracker.Data.Entities.Status, Tracker.Domain.Models.StatusReadModel>();
            CreateMap<Tracker.Domain.Models.StatusCreateModel, Tracker.Data.Entities.Status>();
            CreateMap<Tracker.Data.Entities.Status, Tracker.Domain.Models.StatusUpdateModel>();
            CreateMap<Tracker.Domain.Models.StatusUpdateModel, Tracker.Data.Entities.Status>();
        }

    }
}
