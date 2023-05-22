using System;

namespace Tracker.Core.Definitions;

public interface ITrackCreated
{
    DateTimeOffset Created { get; set; }
    string? CreatedBy { get; set; }
}
