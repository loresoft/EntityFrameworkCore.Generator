using System;
using AutoMapper;
using Tracker.PostgreSQL.Core.Data.Entities;
using Tracker.PostgreSQL.Core.Domain.Models;

namespace Tracker.PostgreSQL.Core.Domain.Mapping
{
    public partial class AuditProfile
        : AutoMapper.Profile
    {
        public AuditProfile()
        {
            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Audit, Tracker.PostgreSQL.Core.Domain.Models.AuditReadModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.AuditCreateModel, Tracker.PostgreSQL.Core.Data.Entities.Audit>();

            CreateMap<Tracker.PostgreSQL.Core.Data.Entities.Audit, Tracker.PostgreSQL.Core.Domain.Models.AuditUpdateModel>();

            CreateMap<Tracker.PostgreSQL.Core.Domain.Models.AuditUpdateModel, Tracker.PostgreSQL.Core.Data.Entities.Audit>();

        }

    }
}
