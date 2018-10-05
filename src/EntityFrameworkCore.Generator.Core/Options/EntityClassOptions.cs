using System;
using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// EntityFramework entity class generation options
    /// </summary>
    /// <seealso cref="ClassOptionsBase" />
    public class EntityClassOptions : ClassOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityClassOptions"/> class.
        /// </summary>
        public EntityClassOptions()
        {
            Namespace = "{Project.Namespace}.Data.Entities";
            Directory = @".\Data\Entities";

            RelationshipNaming = RelationshipNaming.Plural;
            EntityNaming = EntityNaming.Singular;
        }

        /// <summary>
        /// Gets or sets the base class to inherit from.
        /// </summary>
        /// <value>
        /// The base class.
        /// </value>
        public string BaseClass { get; set; }

        /// <summary>
        /// Gets or sets the entity class naming strategy.
        /// </summary>
        /// <value>
        /// The entity class naming strategy.
        /// </value>
        [DefaultValue(EntityNaming.Singular)]
        public EntityNaming EntityNaming { get; set; }

        /// <summary>
        /// Gets or sets the relationship property naming strategy.
        /// </summary>
        /// <value>
        /// The relationship property naming strategy.
        /// </value>
        [DefaultValue(RelationshipNaming.Plural)]
        public RelationshipNaming RelationshipNaming { get; set; }
    }
}