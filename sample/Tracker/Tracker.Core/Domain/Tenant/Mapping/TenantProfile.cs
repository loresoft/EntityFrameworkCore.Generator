using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Mapper class for entity <see cref="Tenant"/> .
/// </summary>
public partial class TenantProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TenantProfile"/> class.
    /// </summary>
    public TenantProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.Tenant, Tracker.Core.Domain.Models.TenantReadModel>();

        CreateMap<Tracker.Core.Domain.Models.TenantCreateModel, Tracker.Core.Data.Entities.Tenant>();

        CreateMap<Tracker.Core.Data.Entities.Tenant, Tracker.Core.Domain.Models.TenantUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.TenantUpdateModel, Tracker.Core.Data.Entities.Tenant>();

    }

}
