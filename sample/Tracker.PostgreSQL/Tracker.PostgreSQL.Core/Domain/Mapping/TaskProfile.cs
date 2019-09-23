using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class TaskProfile
        : AutoMapper.Profile
    {
        public TaskProfile()
        {
            CreateMap<Tracker.Data.Entities.Task, Tracker.Domain.Models.TaskReadModel>();
            CreateMap<Tracker.Domain.Models.TaskCreateModel, Tracker.Data.Entities.Task>();
            CreateMap<Tracker.Data.Entities.Task, Tracker.Domain.Models.TaskUpdateModel>();
            CreateMap<Tracker.Domain.Models.TaskUpdateModel, Tracker.Data.Entities.Task>();
        }

    }
}
