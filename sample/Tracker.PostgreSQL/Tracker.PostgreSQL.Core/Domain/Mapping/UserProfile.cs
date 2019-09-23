using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class UserProfile
        : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<Tracker.Data.Entities.User, Tracker.Domain.Models.UserReadModel>();
            CreateMap<Tracker.Domain.Models.UserCreateModel, Tracker.Data.Entities.User>();
            CreateMap<Tracker.Data.Entities.User, Tracker.Domain.Models.UserUpdateModel>();
            CreateMap<Tracker.Domain.Models.UserUpdateModel, Tracker.Data.Entities.User>();
        }

    }
}
