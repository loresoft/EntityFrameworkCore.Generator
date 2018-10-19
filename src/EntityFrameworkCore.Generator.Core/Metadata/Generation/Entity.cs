using System;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    /// <summary>
    /// An entity model for a database table used when reverse engineering an existing database.
    /// </summary>
    /// <seealso cref="ModelBase" />
    [DebuggerDisplay("Class: {EntityClass}, Table: {TableName}, Context: {ContextProperty}")]
    public class Entity : ModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            Properties = new PropertyCollection<Entity>();
            Relationships = new RelationshipCollection();
            Methods = new MethodCollection();
            Models = new ModelCollection();
        }

        /// <summary>
        /// Gets or sets the parent <see cref="EntityContext"/> this entity belong to.
        /// </summary>
        /// <value>
        /// The parent context this entity belongs to.
        /// </value>
        public EntityContext Context { get; set; }

        /// <summary>
        /// Gets or sets the property name for this entity on the data context.
        /// </summary>
        /// <value>
        /// The property name for this entity on the data context.
        /// </value>
        public string ContextProperty { get; set; }


        public string EntityNamespace { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity class.
        /// </summary>
        /// <value>
        /// The name of the entity class.
        /// </value>
        public string EntityClass { get; set; }

        public string EntityBaseClass { get; set; }


        public string MappingNamespace { get; set; }

        /// <summary>
        /// Gets or sets the name of the table mapping class.
        /// </summary>
        /// <value>
        /// The name of the table mapping class.
        /// </value>
        public string MappingClass { get; set; }


        /// <summary>
        /// Gets or sets the table schema.
        /// </summary>
        /// <value>
        /// The table schema.
        /// </value>
        public string TableSchema { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; }


        /// <summary>
        /// Gets or sets the entity's properties.
        /// </summary>
        /// <value>
        /// The entity's properties.
        /// </value>
        public PropertyCollection<Entity> Properties { get; set; }

        /// <summary>
        /// Gets or sets the entity's relationships.
        /// </summary>
        /// <value>
        /// The entity's relationships.
        /// </value>
        public RelationshipCollection Relationships { get; set; }

        /// <summary>
        /// Gets or sets the entity's methods.
        /// </summary>
        /// <value>
        /// The entity's methods.
        /// </value>
        public MethodCollection Methods { get; set; }


        /// <summary>
        /// Gets or sets the models for this entity.
        /// </summary>
        /// <value>
        /// The models for this entity.
        /// </value>
        public ModelCollection Models { get; set; }

    }
}
