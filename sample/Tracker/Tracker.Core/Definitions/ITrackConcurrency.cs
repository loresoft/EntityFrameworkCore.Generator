using System;

namespace Tracker.Core.Definitions;

public interface ITrackConcurrency
{
    string RowVersion { get; set; }
}