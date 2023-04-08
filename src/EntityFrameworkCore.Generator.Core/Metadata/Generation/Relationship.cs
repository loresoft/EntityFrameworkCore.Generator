using System.Diagnostics;

using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

[DebuggerDisplay("Primary: {PrimaryEntity}, Property: {PropertyName}, Relationship: {RelationshipName}")]
public class Relationship : ModelBase
{
    public Relationship()
    {
        Properties = new PropertyCollection();
        PrimaryProperties = new PropertyCollection();
    }

    public string RelationshipName { get; set; }


    public Entity Entity { get; set; }

    public PropertyCollection Properties { get; set; }

    public string PropertyName { get; set; }

    public Cardinality Cardinality { get; set; }


    public Entity PrimaryEntity { get; set; }

    public PropertyCollection PrimaryProperties { get; set; }

    public string PrimaryPropertyName { get; set; }

    public Cardinality PrimaryCardinality { get; set; }


    public ReferentialAction? ReferentialAction { get; set; }
    public bool IsForeignKey { get; set; }
    public bool IsMapped { get; set; }

    public bool IsOneToOne => Cardinality != Cardinality.Many && PrimaryCardinality != Cardinality.Many;
}
