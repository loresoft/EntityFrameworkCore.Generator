using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class TaskExtendedProfile
        : AutoMapper.Profile
    {
        public TaskExtendedProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended, Tracker.PostgreSQL.Core.Domain.Models.TaskExtendedReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.TaskExtendedCreateModel, Tracker.PostgreSQL.Core.Data.Entities.TaskExtended>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.TaskExtended, Tracker.PostgreSQL.Core.Domain.Models.TaskExtendedUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.TaskExtendedUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.TaskExtended>();

        }

    }
}
