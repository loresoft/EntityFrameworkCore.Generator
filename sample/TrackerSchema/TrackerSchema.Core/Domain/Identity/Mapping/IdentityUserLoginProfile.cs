using System;
using AutoMapper;
using TrackerSchema.Core.Data.Identity.Entities;
using TrackerSchema.Core.Domain.Identity.Models;

namespace TrackerSchema.Core.Domain.Identity.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="IdentityUserLogin"/> .
    /// </summary>
    public partial class IdentityUserLoginProfile
        : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUserLoginProfile"/> class.
        /// </summary>
        public IdentityUserLoginProfile()
        {
            CreateMap<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin, TrackerSchema.Core.Domain.Identity.Models.IdentityUserLoginReadModel>();

            CreateMap<TrackerSchema.Core.Domain.Identity.Models.IdentityUserLoginCreateModel, TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin>();

            CreateMap<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin, TrackerSchema.Core.Domain.Identity.Models.IdentityUserLoginUpdateModel>();

            CreateMap<TrackerSchema.Core.Domain.Identity.Models.IdentityUserLoginUpdateModel, TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin>();

        }

    }
}
