using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.Audit" /> entity mapped to the <c>dbo.Audit</c> table and its generated create, read, and update models.
/// </summary>
public partial class AuditProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.AuditProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.Audit" />.
    /// </summary>
    public AuditProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.Audit, Tracker.Core.Domain.Models.AuditReadModel>();

        CreateMap<Tracker.Core.Domain.Models.AuditCreateModel, Tracker.Core.Data.Entities.Audit>();

        CreateMap<Tracker.Core.Data.Entities.Audit, Tracker.Core.Domain.Models.AuditCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.Audit, Tracker.Core.Domain.Models.AuditUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.AuditUpdateModel, Tracker.Core.Data.Entities.Audit>();

        CreateMap<Tracker.Core.Domain.Models.AuditReadModel, Tracker.Core.Domain.Models.AuditUpdateModel>();

    }

}
