using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.Role" /> entity mapped to the <c>dbo.Role</c> table and its generated create, read, and update models.
/// </summary>
public partial class RoleProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.RoleProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.Role" />.
    /// </summary>
    public RoleProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.Role, Tracker.Core.Domain.Models.RoleReadModel>();

        CreateMap<Tracker.Core.Domain.Models.RoleCreateModel, Tracker.Core.Data.Entities.Role>();

        CreateMap<Tracker.Core.Data.Entities.Role, Tracker.Core.Domain.Models.RoleCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.Role, Tracker.Core.Domain.Models.RoleUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.RoleUpdateModel, Tracker.Core.Data.Entities.Role>();

        CreateMap<Tracker.Core.Domain.Models.RoleReadModel, Tracker.Core.Domain.Models.RoleUpdateModel>();

    }

}
