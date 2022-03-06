using System;

namespace Tracker.Core.Definitions
{
    public interface ITrackUpdated
    {
        DateTimeOffset Updated { get; set; }
        string? UpdatedBy { get; set; }
    }
}
