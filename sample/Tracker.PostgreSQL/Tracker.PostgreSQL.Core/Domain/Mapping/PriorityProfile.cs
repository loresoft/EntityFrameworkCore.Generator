using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class PriorityProfile
        : AutoMapper.Profile
    {
        public PriorityProfile()
        {
            CreateMap<Tracker.Data.Entities.Priority, Tracker.Domain.Models.PriorityReadModel>();
            CreateMap<Tracker.Domain.Models.PriorityCreateModel, Tracker.Data.Entities.Priority>();
            CreateMap<Tracker.Data.Entities.Priority, Tracker.Domain.Models.PriorityUpdateModel>();
            CreateMap<Tracker.Domain.Models.PriorityUpdateModel, Tracker.Data.Entities.Priority>();
        }

    }
}
