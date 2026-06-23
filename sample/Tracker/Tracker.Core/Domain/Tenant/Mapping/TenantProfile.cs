using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.Tenant" /> entity mapped to the <c>dbo.Tenant</c> table and its generated create, read, and update models.
/// </summary>
public partial class TenantProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.TenantProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.Tenant" />.
    /// </summary>
    public TenantProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.Tenant, Tracker.Core.Domain.Models.TenantReadModel>();

        CreateMap<Tracker.Core.Domain.Models.TenantCreateModel, Tracker.Core.Data.Entities.Tenant>();

        CreateMap<Tracker.Core.Data.Entities.Tenant, Tracker.Core.Domain.Models.TenantCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.Tenant, Tracker.Core.Domain.Models.TenantUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.TenantUpdateModel, Tracker.Core.Data.Entities.Tenant>();

        CreateMap<Tracker.Core.Domain.Models.TenantReadModel, Tracker.Core.Domain.Models.TenantUpdateModel>();

    }

}
