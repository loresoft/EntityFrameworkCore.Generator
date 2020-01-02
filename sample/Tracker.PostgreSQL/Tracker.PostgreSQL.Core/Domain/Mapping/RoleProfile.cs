using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class RoleProfile
        : AutoMapper.Profile
    {
        public RoleProfile()
        {
            CreateMap<Tracker.Data.Entities.Role, Tracker.Domain.Models.RoleReadModel>();
            CreateMap<Tracker.Domain.Models.RoleCreateModel, Tracker.Data.Entities.Role>();
            CreateMap<Tracker.Data.Entities.Role, Tracker.Domain.Models.RoleUpdateModel>();
            CreateMap<Tracker.Domain.Models.RoleUpdateModel, Tracker.Data.Entities.Role>();
        }

    }
}
