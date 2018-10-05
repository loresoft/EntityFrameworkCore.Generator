using System;
using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    /// <summary>
    /// An entity model for a database table used when reverse engineering an existing database.
    /// </summary>
    /// <seealso cref="ModelBase" />
    [DebuggerDisplay("Class: {EntityClass}, Table: {FullName}, Context: {ContextName}")]
    public class Entity : ModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            Properties = new PropertyCollection();
            Relationships = new RelationshipCollection();
            Methods = new MethodCollection();
        }


        /// <summary>
        /// Gets or sets the property name for this entity on the data context.
        /// </summary>
        /// <value>
        /// The property name for this entity on the data context.
        /// </value>
        public string ContextName { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity class.
        /// </summary>
        /// <value>
        /// The name of the entity class.
        /// </value>
        public string EntityClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the table mapping class.
        /// </summary>
        /// <value>
        /// The name of the table mapping class.
        /// </value>
        public string MappingClass { get; set; }


        /// <summary>
        /// Gets or sets the read view model class name.
        /// </summary>
        /// <value>
        /// The read view model class name.
        /// </value>
        public string ReadModel { get; set; }

        /// <summary>
        /// Gets or sets the create view model class name.
        /// </summary>
        /// <value>
        /// The create view model class name.
        /// </value>
        public string CreateModel { get; set; }

        /// <summary>
        /// Gets or sets the edit view model class name.
        /// </summary>
        /// <value>
        /// The edit view model class name.
        /// </value>
        public string EditModel { get; set; }


        
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
        /// Gets or sets the table full name.
        /// </summary>
        /// <value>
        /// The table full name.
        /// </value>
        public string FullName { get; set; }


        /// <summary>
        /// Gets or sets the entity's properties.
        /// </summary>
        /// <value>
        /// The entity's properties.
        /// </value>
        public PropertyCollection Properties { get; set; }

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
    }
}
