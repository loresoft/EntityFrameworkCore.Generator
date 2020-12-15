using System;
using System.Collections.Generic;
using Tracker.Core.Definitions;

namespace Tracker.Core.Data.Entities
{
    /// <summary>
    /// Entity class representing data for table 'Task'.
    /// </summary>
    public partial class Task : IHaveIdentifier, ITrackCreated, ITrackUpdated
        { }
}
