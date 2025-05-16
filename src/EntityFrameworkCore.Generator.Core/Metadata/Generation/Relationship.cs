using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

[DebuggerDisplay("Primary: {PrimaryEntity}, Property: {PropertyName}, Relationship: {RelationshipName}")]
public class Relationship : ModelBase
{
    public Relationship()
    {
        Properties = [];
        PrimaryProperties = [];
    }

    public string? RelationshipName { get; set; }


    public Entity Entity { get; set; } = null!;

    public PropertyCollection Properties { get; set; }

    public string PropertyName { get; set; } = null!;

    public Cardinality Cardinality { get; set; }


    public Entity PrimaryEntity { get; set; } = null!;

    public PropertyCollection PrimaryProperties { get; set; }

    public string PrimaryPropertyName { get; set; } = null!;

    public Cardinality PrimaryCardinality { get; set; }


    public bool? CascadeDelete { get; set; }
    public bool IsForeignKey { get; set; }
    public bool IsMapped { get; set; }

    public bool IsOneToOne => Cardinality != Cardinality.Many && PrimaryCardinality != Cardinality.Many;
}
