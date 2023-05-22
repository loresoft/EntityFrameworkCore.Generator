using System;

namespace Tracker.Core.Definitions;

public interface ITrackDeleted
{
    bool IsDeleted { get; set; }
}