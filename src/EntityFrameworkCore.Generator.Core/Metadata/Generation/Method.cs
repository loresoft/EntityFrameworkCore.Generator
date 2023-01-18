using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

[DebuggerDisplay("Suffix: {NameSuffix}, IsKey: {IsKey}, IsUnique: {IsUnique}")]
public class Method : ModelBase
{
    public Method()
    {
        Properties = new PropertyCollection();
    }

    public Entity Entity { get; set; }

    public string NameSuffix { get; set; }
    public string SourceName { get; set; }

    public bool IsKey { get; set; }
    public bool IsUnique { get; set; }
    public bool IsIndex { get; set; }

    public PropertyCollection Properties { get; set; }
}