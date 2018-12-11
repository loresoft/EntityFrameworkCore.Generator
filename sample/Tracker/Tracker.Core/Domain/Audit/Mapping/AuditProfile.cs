using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Mapping
{
    /// <summary>
    /// Mapper class for entity <see cref="Audit"/> .
    /// </summary>
    public partial class AuditProfile
        : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditProfile"/> class.
        /// </summary>
        public AuditProfile()
        {
            CreateMap<Audit, AuditReadModel>();
            CreateMap<AuditCreateModel, Audit>();
            CreateMap<AuditUpdateModel, Audit>();
        }

    }
}
