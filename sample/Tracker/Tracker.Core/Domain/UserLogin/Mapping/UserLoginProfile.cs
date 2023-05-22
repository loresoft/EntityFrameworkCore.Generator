using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Mapper class for entity <see cref="UserLogin"/> .
/// </summary>
public partial class UserLoginProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserLoginProfile"/> class.
    /// </summary>
    public UserLoginProfile()
    {
        CreateMap<UserLogin, UserLoginReadModel>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));
    }

}
