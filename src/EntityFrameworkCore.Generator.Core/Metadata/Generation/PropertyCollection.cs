using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    /// <summary>
    /// A collection of <see cref="Property"/> instances
    /// </summary>
    public class PropertyCollection
      : List<Property>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyCollection"/> class.
        /// </summary>
        public PropertyCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        public PropertyCollection(IEnumerable<Property> collection) : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyCollection"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public PropertyCollection(List<Property> list) : base(list)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// Gets the primary keys properties.
        /// </summary>
        /// <value>
        /// The primary keys properties.
        /// </value>
        public IEnumerable<Property> PrimaryKeys
        {
            get { return this.Where(p => p.IsPrimaryKey == true); }
        }

        /// <summary>
        /// Gets the foreign keys properties.
        /// </summary>
        /// <value>
        /// The foreign keys.
        /// </value>
        public IEnumerable<Property> ForeignKeys
        {
            get { return this.Where(p => p.IsForeignKey == true); }
        }

        /// <summary>
        /// Gets the property by column name
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public Property ByColumn(string columnName)
        {
            return this.FirstOrDefault(x => x.ColumnName == columnName);
        }

        /// <summary>
        /// Gets the property by property name
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public Property ByProperty(string propertyName)
        {
            return this.FirstOrDefault(x => x.PropertyName == propertyName);
        }
    }
}
