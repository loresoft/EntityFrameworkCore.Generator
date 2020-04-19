using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class StatusProfile
        : AutoMapper.Profile
    {
        public StatusProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Status, Tracker.PostgreSQL.Core.Domain.Models.StatusReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.StatusCreateModel, Tracker.PostgreSQL.Core.Data.Entities.Status>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Status, Tracker.PostgreSQL.Core.Domain.Models.StatusUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.StatusUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.Status>();

        }

    }
}
