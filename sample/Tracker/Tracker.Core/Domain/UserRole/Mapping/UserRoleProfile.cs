using System;
using AutoMapper;
using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Mapper class for entity <see cref="UserRole"/> .
/// </summary>
public partial class UserRoleProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleProfile"/> class.
    /// </summary>
    public UserRoleProfile()
    {
        CreateMap<UserRole, UserRoleReadModel>();
        CreateMap<UserRoleCreateModel, UserRole>();
        CreateMap<UserRoleUpdateModel, UserRole>();
    }

}
