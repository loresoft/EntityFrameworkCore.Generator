using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="Task"/> .
    /// </summary>
    public partial class TaskProfile
        : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskProfile"/> class.
        /// </summary>
        public TaskProfile()
        {
            CreateMap<Task, TaskReadModel>();
            CreateMap<TaskCreateModel, Task>();
            CreateMap<TaskUpdateModel, Task>();
        }

    }
}
