using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class RoleProfile
        : AutoMapper.Profile
    {
        public RoleProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Role, Tracker.PostgreSQL.Core.Domain.Models.RoleReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.RoleCreateModel, Tracker.PostgreSQL.Core.Data.Entities.Role>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Role, Tracker.PostgreSQL.Core.Domain.Models.RoleUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.RoleUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.Role>();

        }

    }
}
