using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class UserRoleProfile
        : AutoMapper.Profile
    {
        public UserRoleProfile()
        {
            CreateMap<Tracker.Data.Entities.UserRole, Tracker.Domain.Models.UserRoleReadModel>();
            CreateMap<Tracker.Domain.Models.UserRoleCreateModel, Tracker.Data.Entities.UserRole>();
            CreateMap<Tracker.Data.Entities.UserRole, Tracker.Domain.Models.UserRoleUpdateModel>();
            CreateMap<Tracker.Domain.Models.UserRoleUpdateModel, Tracker.Data.Entities.UserRole>();
        }

    }
}
