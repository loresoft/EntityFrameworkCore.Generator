using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Mapper class for entity <see cref="Role"/> .
/// </summary>
public partial class RoleProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleProfile"/> class.
    /// </summary>
    public RoleProfile()
    {
        CreateMap<Role, RoleReadModel>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

        CreateMap<RoleCreateModel, Role>();

        CreateMap<RoleUpdateModel, Role>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));
    }

}
