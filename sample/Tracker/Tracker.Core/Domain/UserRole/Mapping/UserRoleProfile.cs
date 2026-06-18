using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.UserRole" /> entity mapped to the <c>dbo.UserRole</c> table and its generated create, read, and update models.
/// </summary>
public partial class UserRoleProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.UserRoleProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.UserRole" />.
    /// </summary>
    public UserRoleProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.UserRole, Tracker.Core.Domain.Models.UserRoleReadModel>();

        CreateMap<Tracker.Core.Domain.Models.UserRoleCreateModel, Tracker.Core.Data.Entities.UserRole>();

        CreateMap<Tracker.Core.Data.Entities.UserRole, Tracker.Core.Domain.Models.UserRoleCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.UserRole, Tracker.Core.Domain.Models.UserRoleUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.UserRoleUpdateModel, Tracker.Core.Data.Entities.UserRole>();

        CreateMap<Tracker.Core.Domain.Models.UserRoleReadModel, Tracker.Core.Domain.Models.UserRoleUpdateModel>();

    }

}
