using System;

using AutoMapper;

using Tracker.Core.Data.Entities;
using Tracker.Core.Domain.Models;

namespace Tracker.Core.Domain.Mapping;

/// <summary>
/// Configures AutoMapper mappings for the <see cref="Tracker.Core.Data.Entities.UserLogin" /> entity mapped to the <c>dbo.UserLogin</c> table and its generated read models.
/// </summary>
public partial class UserLoginProfile
    : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tracker.Core.Domain.Mapping.UserLoginProfile"/> class and creates mappings for <see cref="Tracker.Core.Data.Entities.UserLogin" />.
    /// </summary>
    public UserLoginProfile()
    {
        CreateMap<Tracker.Core.Data.Entities.UserLogin, Tracker.Core.Domain.Models.UserLoginReadModel>();

    }

}
