using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Parsing;

[DebuggerDisplay("Property: {PropertyName}")]
public class ParsedModelProperty
{
    public ParsedModelProperty()
    {
        Attributes = [];
    }

    public string PropertyName { get; set; } = null!;

    public List<string> Attributes { get; }
}
