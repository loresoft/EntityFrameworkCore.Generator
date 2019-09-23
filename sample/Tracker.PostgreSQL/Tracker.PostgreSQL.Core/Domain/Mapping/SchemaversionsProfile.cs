using System;
using AutoMapper;
using Tracker.Data.Entities;
using Tracker.Domain.Models;

namespace Tracker.Domain.Mapping
{
    public partial class SchemaversionsProfile
        : AutoMapper.Profile
    {
        public SchemaversionsProfile()
        {
            CreateMap<Tracker.Data.Entities.Schemaversions, Tracker.Domain.Models.SchemaversionsReadModel>();
            CreateMap<Tracker.Domain.Models.SchemaversionsCreateModel, Tracker.Data.Entities.Schemaversions>();
            CreateMap<Tracker.Data.Entities.Schemaversions, Tracker.Domain.Models.SchemaversionsUpdateModel>();
            CreateMap<Tracker.Domain.Models.SchemaversionsUpdateModel, Tracker.Data.Entities.Schemaversions>();
        }

    }
}
