using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

[DebuggerDisplay("Class: {ContextClass}, Database: {DatabaseName}")]
public class EntityContext : ModelBase
{
    public EntityContext()
    {
        Entities = [];
    }

    public string ContextNamespace { get; set; } = null!;

    public string ContextClass { get; set; } = null!;

    public string? ContextBaseClass { get; set; }

    public string? DatabaseName { get; set; }

    public EntityCollection Entities { get; set; }

}
