using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Mapping;

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
        CreateMap<Task, TaskReadModel>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)))
            .ForMember(d => d.PriorityName, opt => opt.MapFrom(s => s.Priority != null ? s.Priority.Name : default))
            .ForMember(d => d.StatusName, opt => opt.MapFrom(s => s.Status.Name))
            .ForMember(d => d.AssignedEmail, opt => opt.MapFrom(s => s.AssignedUser != null ? s.AssignedUser.EmailAddress : default));

        CreateMap<TaskCreateModel, Task>();

        CreateMap<TaskUpdateModel, Task>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));
    }

}
