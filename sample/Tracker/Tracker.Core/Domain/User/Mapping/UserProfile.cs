using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.User" /> entity mapped to the <c>dbo.User</c> table and its generated create, read, and update models.
/// </summary>
public partial class UserProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.UserProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.User" />.
    /// </summary>
    public UserProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.User, Tracker.Core.Domain.Models.UserReadModel>();

        CreateMap<Tracker.Core.Domain.Models.UserCreateModel, Tracker.Core.Data.Entities.User>();

        CreateMap<Tracker.Core.Data.Entities.User, Tracker.Core.Domain.Models.UserCreateModel>();

        CreateMap<Tracker.Core.Data.Entities.User, Tracker.Core.Domain.Models.UserUpdateModel>();

        CreateMap<Tracker.Core.Domain.Models.UserUpdateModel, Tracker.Core.Data.Entities.User>();

        CreateMap<Tracker.Core.Domain.Models.UserReadModel, Tracker.Core.Domain.Models.UserUpdateModel>();

    }

}
