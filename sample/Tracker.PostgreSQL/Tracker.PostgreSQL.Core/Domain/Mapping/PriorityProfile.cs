using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class PriorityProfile
        : AutoMapper.Profile
    {
        public PriorityProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Priority, Tracker.PostgreSQL.Core.Domain.Models.PriorityReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.PriorityCreateModel, Tracker.PostgreSQL.Core.Data.Entities.Priority>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Priority, Tracker.PostgreSQL.Core.Domain.Models.PriorityUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.PriorityUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.Priority>();

        }

    }
}
