using System;
using System.Collections.Generic;

namespace Tracker.Scaffold;

public partial class Task
{
    public Guid Id { get; set; }

    public Guid StatusId { get; set; }

    public Guid? PriorityId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public DateTimeOffset? CompleteDate { get; set; }

    public Guid? AssignedId { get; set; }

    public Guid TenantId { get; set; }

    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual User? Assigned { get; set; }

    public virtual Priority? Priority { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual TaskExtended? TaskExtended { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
