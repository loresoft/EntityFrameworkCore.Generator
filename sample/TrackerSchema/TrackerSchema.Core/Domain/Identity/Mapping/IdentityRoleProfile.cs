using System;
using AutoMapper;
using TrackerSchema.Core.Data.Identity.Entities;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="IdentityRole"/> .
    /// </summary>
    public partial class IdentityRoleProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityRoleProfile"/> class.
        /// </summary>
        public IdentityRoleProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Identity.Entities.IdentityRole, TrackerSchema.Core.Domain.Identity.Models.IdentityRoleReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Identity.Models.IdentityRoleCreateModel, TrackerSchema.Core.Data.Identity.Entities.IdentityRole>();

            CreateMap<TrackerSchema.Core.Data.Identity.Entities.IdentityRole, TrackerSchema.Core.Domain.Identity.Models.IdentityRoleUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Identity.Models.IdentityRoleUpdateModel, TrackerSchema.Core.Data.Identity.Entities.IdentityRole>();

        }

    }
}
