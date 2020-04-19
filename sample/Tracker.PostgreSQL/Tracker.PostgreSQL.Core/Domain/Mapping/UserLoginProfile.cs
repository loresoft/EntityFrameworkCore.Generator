using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class UserLoginProfile
        : AutoMapper.Profile
    {
        public UserLoginProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.UserLogin, Tracker.PostgreSQL.Core.Domain.Models.UserLoginReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.UserLoginCreateModel, Tracker.PostgreSQL.Core.Data.Entities.UserLogin>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.UserLogin, Tracker.PostgreSQL.Core.Domain.Models.UserLoginUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.UserLoginUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.UserLogin>();

        }

    }
}
