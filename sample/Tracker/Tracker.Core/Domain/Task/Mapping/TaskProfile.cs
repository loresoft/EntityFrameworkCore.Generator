using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.Task" /> entity mapped to the <c>dbo.Task</c> table and its generated create, read, and update models.
/// </summary>
public partial class TaskProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.TaskProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.Task" />.
    /// </summary>
    public TaskProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.Task, Tracker.Core.Domain.Models.TaskReadModel>();

        CreateMap<Tracker.Core.Domain.Models.TaskCreateModel, Tracker.Core.Data.Entities.Task>();

        CreateMap<Tracker.Core.Data.Entities.Task, Tracker.Core.Domain.Models.TaskCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.Task, Tracker.Core.Domain.Models.TaskUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.TaskUpdateModel, Tracker.Core.Data.Entities.Task>();

        CreateMap<Tracker.Core.Domain.Models.TaskReadModel, Tracker.Core.Domain.Models.TaskUpdateModel>();

    }

}
