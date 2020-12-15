using System;
using Tracker.Core.Definitions;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models
{
    public class EntityReadModel : IHaveIdentifier, ITrackCreated, ITrackUpdated, ITrackConcurrency
    {
        public Guid Id { get; set; }

        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

        public string UpdatedBy { get; set; }

        public string RowVersion { get; set; }
    }
}