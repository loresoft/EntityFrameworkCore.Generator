using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="Status"/> .
    /// </summary>
    public partial class StatusProfile
        : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusProfile"/> class.
        /// </summary>
        public StatusProfile()
        {
            CreateMap<Status, StatusReadModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<StatusCreateModel, Status>();

            CreateMap<StatusUpdateModel, Status>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));
        }

    }
}
