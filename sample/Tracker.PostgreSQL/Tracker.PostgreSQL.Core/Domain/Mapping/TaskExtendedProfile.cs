using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class TaskExtendedProfile
        : AutoMapper.Profile
    {
        public TaskExtendedProfile()
        {
            CreateMap<Tracker.Data.Entities.TaskExtended, Tracker.Domain.Models.TaskExtendedReadModel>();
            CreateMap<Tracker.Domain.Models.TaskExtendedCreateModel, Tracker.Data.Entities.TaskExtended>();
            CreateMap<Tracker.Data.Entities.TaskExtended, Tracker.Domain.Models.TaskExtendedUpdateModel>();
            CreateMap<Tracker.Domain.Models.TaskExtendedUpdateModel, Tracker.Data.Entities.TaskExtended>();
        }

    }
}
