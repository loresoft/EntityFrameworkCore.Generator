using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.Priority" /> entity mapped to the <c>dbo.Priority</c> table and its generated create, read, and update models.
/// </summary>
public partial class PriorityProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.PriorityProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.Priority" />.
    /// </summary>
    public PriorityProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.Priority, Tracker.Core.Domain.Models.PriorityReadModel>();

        CreateMap<Tracker.Core.Domain.Models.PriorityCreateModel, Tracker.Core.Data.Entities.Priority>();

        CreateMap<Tracker.Core.Data.Entities.Priority, Tracker.Core.Domain.Models.PriorityCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.Priority, Tracker.Core.Domain.Models.PriorityUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.PriorityUpdateModel, Tracker.Core.Data.Entities.Priority>();

        CreateMap<Tracker.Core.Domain.Models.PriorityReadModel, Tracker.Core.Domain.Models.PriorityUpdateModel>();

    }

}
