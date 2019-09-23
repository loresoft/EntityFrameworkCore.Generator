using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class AuditProfile
        : AutoMapper.Profile
    {
        public AuditProfile()
        {
            CreateMap<Tracker.Data.Entities.Audit, Tracker.Domain.Models.AuditReadModel>();
            CreateMap<Tracker.Domain.Models.AuditCreateModel, Tracker.Data.Entities.Audit>();
            CreateMap<Tracker.Data.Entities.Audit, Tracker.Domain.Models.AuditUpdateModel>();
            CreateMap<Tracker.Domain.Models.AuditUpdateModel, Tracker.Data.Entities.Audit>();
        }

    }
}
