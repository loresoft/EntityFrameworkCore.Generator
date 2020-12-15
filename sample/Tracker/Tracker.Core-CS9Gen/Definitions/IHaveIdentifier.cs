using System;

namespace Tracker.Core.Definitions
{
    public interface IHaveIdentifier
    {
        Guid Id { get; set; }
    }
}