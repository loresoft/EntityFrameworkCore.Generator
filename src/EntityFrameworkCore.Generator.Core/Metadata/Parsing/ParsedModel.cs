using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Parsing;

[DebuggerDisplay("Model: {ModelClass}, Properties: {Properties.Count}")]
public class ParsedModel
{
    public ParsedModel()
    {
        Properties = [];
    }

    public string ModelClass { get; set; } = null!;

    public List<ParsedModelProperty> Properties { get; }
}
