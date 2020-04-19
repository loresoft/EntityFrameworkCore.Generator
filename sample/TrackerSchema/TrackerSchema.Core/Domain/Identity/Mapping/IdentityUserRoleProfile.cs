using System;
using AutoMapper;
using TrackerSchema.Core.Data.Identity.Entities;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="IdentityUserRole"/> .
    /// </summary>
    public partial class IdentityUserRoleProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserRoleProfile"/> class.
        /// </summary>
        public IdentityUserRoleProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole, TrackerSchema.Core.Domain.Identity.Models.IdentityUserRoleReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Identity.Models.IdentityUserRoleCreateModel, TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole>();

            CreateMap<TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole, TrackerSchema.Core.Domain.Identity.Models.IdentityUserRoleUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Identity.Models.IdentityUserRoleUpdateModel, TrackerSchema.Core.Data.Identity.Entities.IdentityUserRole>();

        }

    }
}
