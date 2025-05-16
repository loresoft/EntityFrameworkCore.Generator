using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Parsing;

[DebuggerDisplay("Context: {ContextClass}")]
public class ParsedContext
{
    public ParsedContext()
    {
        Properties = [];
    }

    public string ContextClass { get; set; } = null!;

    public List<ParsedEntitySet> Properties { get; }
}
