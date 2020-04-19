using System;
using AutoMapper;
using TrackerSchema.Core.Data.Identity.Entities;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="IdentityUser"/> .
    /// </summary>
    public partial class IdentityUserProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserProfile"/> class.
        /// </summary>
        public IdentityUserProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Identity.Entities.IdentityUser, TrackerSchema.Core.Domain.Identity.Models.IdentityUserReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Identity.Models.IdentityUserCreateModel, TrackerSchema.Core.Data.Identity.Entities.IdentityUser>();

            CreateMap<TrackerSchema.Core.Data.Identity.Entities.IdentityUser, TrackerSchema.Core.Domain.Identity.Models.IdentityUserUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Identity.Models.IdentityUserUpdateModel, TrackerSchema.Core.Data.Identity.Entities.IdentityUser>();

        }

    }
}
