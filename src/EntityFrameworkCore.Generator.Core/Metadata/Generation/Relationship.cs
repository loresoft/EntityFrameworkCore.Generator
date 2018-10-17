using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    [DebuggerDisplay("Primary: {PrimaryEntity}, Property: {PropertyName}, Relationship: {RelationshipName}")]
    public class Relationship : ModelBase
    {
        public Relationship()
        {
            Properties = new PropertyCollection<Entity>();
            PrimaryProperties = new PropertyCollection<Entity>();
        }

        public string RelationshipName { get; set; }


        public Entity Entity { get; set; }

        public PropertyCollection<Entity> Properties { get; set; }

        public string PropertyName { get; set; }

        public Cardinality Cardinality { get; set; }


        public Entity PrimaryEntity { get; set; }

        public PropertyCollection<Entity> PrimaryProperties { get; set; }

        public string PrimaryPropertyName { get; set; }

        public Cardinality PrimaryCardinality { get; set; }


        public bool? CascadeDelete { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsMapped { get; set; }

        public bool IsOneToOne => Cardinality != Cardinality.Many && PrimaryCardinality != Cardinality.Many;
    }
}
