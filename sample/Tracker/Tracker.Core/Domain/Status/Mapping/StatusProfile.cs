using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.Status" /> entity mapped to the <c>dbo.Status</c> table and its generated create, read, and update models.
/// </summary>
public partial class StatusProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.StatusProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.Status" />.
    /// </summary>
    public StatusProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.Status, Tracker.Core.Domain.Models.StatusReadModel>();

        CreateMap<Tracker.Core.Domain.Models.StatusCreateModel, Tracker.Core.Data.Entities.Status>();

        CreateMap<Tracker.Core.Data.Entities.Status, Tracker.Core.Domain.Models.StatusCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.Status, Tracker.Core.Domain.Models.StatusUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.StatusUpdateModel, Tracker.Core.Data.Entities.Status>();

        CreateMap<Tracker.Core.Domain.Models.StatusReadModel, Tracker.Core.Domain.Models.StatusUpdateModel>();

    }

}
