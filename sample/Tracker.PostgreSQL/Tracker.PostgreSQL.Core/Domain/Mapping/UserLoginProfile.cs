using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class UserLoginProfile
        : AutoMapper.Profile
    {
        public UserLoginProfile()
        {
            CreateMap<Tracker.Data.Entities.UserLogin, Tracker.Domain.Models.UserLoginReadModel>();
            CreateMap<Tracker.Domain.Models.UserLoginCreateModel, Tracker.Data.Entities.UserLogin>();
            CreateMap<Tracker.Data.Entities.UserLogin, Tracker.Domain.Models.UserLoginUpdateModel>();
            CreateMap<Tracker.Domain.Models.UserLoginUpdateModel, Tracker.Data.Entities.UserLogin>();
        }

    }
}
