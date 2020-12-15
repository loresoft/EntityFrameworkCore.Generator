using System;
using Tracker.Core.Definitions;

// ReSharper disable once CheckNamespace
namespace Tracker.Core.Domain.Models
{
    public class EntityCreateModel : IHaveIdentifier, ITrackCreated, ITrackUpdated
    {
        public Guid Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }
    }
}