using System;
using System.Collections.Generic;
using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'User'.
    /// </summary>
    public partial class User : IHaveIdentifier, ITrackCreated, ITrackUpdated
    { }
}
