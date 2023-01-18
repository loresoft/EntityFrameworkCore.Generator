using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Parsing;

[DebuggerDisplay("This: {ThisPropertyName}, Other: {OtherPropertyName}")]
public class ParsedRelationship
{
    public ParsedRelationship()
    {
        ThisProperties = new List<string>();
    }

    public string ThisPropertyName { get; set; }

    public List<string> ThisProperties { get; }

    public string OtherPropertyName { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(ThisPropertyName)
               && !string.IsNullOrEmpty(OtherPropertyName)
               && ThisProperties.Count > 0;
    }

}