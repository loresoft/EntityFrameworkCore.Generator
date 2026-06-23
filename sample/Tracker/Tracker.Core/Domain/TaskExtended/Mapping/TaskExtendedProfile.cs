using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entity mapped to the <c>dbo.TaskExtended</c> table and its generated create, read, and update models.
/// </summary>
public partial class TaskExtendedProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.TaskExtendedProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.TaskExtended" />.
    /// </summary>
    public TaskExtendedProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.TaskExtended, Tracker.Core.Domain.Models.TaskExtendedReadModel>();

        CreateMap<Tracker.Core.Domain.Models.TaskExtendedCreateModel, Tracker.Core.Data.Entities.TaskExtended>();

        CreateMap<Tracker.Core.Data.Entities.TaskExtended, Tracker.Core.Domain.Models.TaskExtendedCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.TaskExtended, Tracker.Core.Domain.Models.TaskExtendedUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.TaskExtendedUpdateModel, Tracker.Core.Data.Entities.TaskExtended>();

        CreateMap<Tracker.Core.Domain.Models.TaskExtendedReadModel, Tracker.Core.Domain.Models.TaskExtendedUpdateModel>();

    }

}
