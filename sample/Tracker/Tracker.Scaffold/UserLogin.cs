using System;
using System.Collections.Generic;

namespace Tracker.Scaffold;

public partial class UserLogin
{
    public Guid Id { get; set; }

    public string EmailAddress { get; set; } = null!;

    public Guid? UserId { get; set; }

    public string? UserAgent { get; set; }

    public string? Browser { get; set; }

    public string? OperatingSystem { get; set; }

    public string? DeviceFamily { get; set; }

    public string? DeviceBrand { get; set; }

    public string? DeviceModel { get; set; }

    public string? IpAddress { get; set; }

    public bool IsSuccessful { get; set; }

    public string? FailureMessage { get; set; }

    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual User? User { get; set; }
}
