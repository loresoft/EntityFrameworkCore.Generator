using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Parsing;

[DebuggerDisplay("Name: {RelationshipName}, Property: {PropertyName}, Primary: {PrimaryPropertyName}")]
public class ParsedRelationship
{
    public ParsedRelationship()
    {
        Properties = new List<string>();
    }
    public string RelationshipName { get; set; }

    public string PropertyName { get; set; }

    public List<string> Properties { get; }

    public string PrimaryPropertyName { get; set; }


    public bool IsValid()
    {
        return !string.IsNullOrEmpty(PropertyName)
               && !string.IsNullOrEmpty(PrimaryPropertyName)
               && Properties.Count > 0;
    }

}
