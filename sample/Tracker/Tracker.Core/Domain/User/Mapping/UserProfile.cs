using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="User"/> .
    /// </summary>
    public partial class UserProfile
        : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// </summary>
        public UserProfile()
        {
            CreateMap<User, UserReadModel>();
            CreateMap<UserCreateModel, User>();
            CreateMap<UserUpdateModel, User>();
        }

    }
}
