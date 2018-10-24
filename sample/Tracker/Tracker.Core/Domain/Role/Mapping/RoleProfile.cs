using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="Role"/> .
    /// </summary>
    public partial class RoleProfile
        : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleProfile"/> class.
        /// </summary>
        public RoleProfile()
        {
            CreateMap<Role, RoleReadModel>();
            CreateMap<RoleCreateModel, Role>();
            CreateMap<RoleUpdateModel, Role>();
        }

    }
}
