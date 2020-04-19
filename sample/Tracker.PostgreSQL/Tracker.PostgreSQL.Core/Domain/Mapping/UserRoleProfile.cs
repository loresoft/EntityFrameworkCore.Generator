using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class UserRoleProfile
        : AutoMapper.Profile
    {
        public UserRoleProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.UserRole, Tracker.PostgreSQL.Core.Domain.Models.UserRoleReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.UserRoleCreateModel, Tracker.PostgreSQL.Core.Data.Entities.UserRole>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.UserRole, Tracker.PostgreSQL.Core.Domain.Models.UserRoleUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.UserRoleUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.UserRole>();

        }

    }
}
