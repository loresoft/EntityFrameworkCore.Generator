using System;
using System.Collections.Generic;

namespace Tracker.Scaffold;

public partial class SchemaVersion
{
    public int Id { get; set; }

    public string ScriptName { get; set; } = null!;

    public DateTime Applied { get; set; }
}
