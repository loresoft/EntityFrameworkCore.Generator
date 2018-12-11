using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="TaskExtended"/> .
    /// </summary>
    public partial class TaskExtendedProfile
        : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskExtendedProfile"/> class.
        /// </summary>
        public TaskExtendedProfile()
        {
            CreateMap<TaskExtended, TaskExtendedReadModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<TaskExtendedCreateModel, TaskExtended>();

            CreateMap<TaskExtendedUpdateModel, TaskExtended>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));
        }

    }
}
