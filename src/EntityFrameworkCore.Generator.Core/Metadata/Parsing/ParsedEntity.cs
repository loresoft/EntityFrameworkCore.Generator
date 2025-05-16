using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Parsing;

[DebuggerDisplay("Table: {TableName}, Entity: {EntityClass}, Mapping: {MappingClass}")]
public class ParsedEntity
{
    public ParsedEntity()
    {
        Properties = [];
        Relationships = [];
    }

    public string EntityClass { get; set; } = null!;
    public string MappingClass { get; set; } = null!;

    public string TableName { get; set; } = null!;
    public string? TableSchema { get; set; }

    public List<ParsedProperty> Properties { get; }
    public List<ParsedRelationship> Relationships { get; }
}
