using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class TaskProfile
        : AutoMapper.Profile
    {
        public TaskProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Task, Tracker.PostgreSQL.Core.Domain.Models.TaskReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.TaskCreateModel, Tracker.PostgreSQL.Core.Data.Entities.Task>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Task, Tracker.PostgreSQL.Core.Domain.Models.TaskUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.TaskUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.Task>();

        }

    }
}
