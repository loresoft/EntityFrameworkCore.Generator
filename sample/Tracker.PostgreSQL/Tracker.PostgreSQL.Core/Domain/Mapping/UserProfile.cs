using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class UserProfile
        : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.User, Tracker.PostgreSQL.Core.Domain.Models.UserReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.UserCreateModel, Tracker.PostgreSQL.Core.Data.Entities.User>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.User, Tracker.PostgreSQL.Core.Domain.Models.UserUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.UserUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.User>();

        }

    }
}
