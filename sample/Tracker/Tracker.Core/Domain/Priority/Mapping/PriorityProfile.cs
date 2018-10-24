using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="Priority"/> .
    /// </summary>
    public partial class PriorityProfile
        : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityProfile"/> class.
        /// </summary>
        public PriorityProfile()
        {
            CreateMap<Priority, PriorityReadModel>();
            CreateMap<PriorityCreateModel, Priority>();
            CreateMap<PriorityUpdateModel, Priority>();
        }

    }
}
