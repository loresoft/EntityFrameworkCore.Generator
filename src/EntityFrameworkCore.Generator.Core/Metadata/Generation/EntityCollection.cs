using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    /// <summary>
    /// A collection of <see cref="Entity"/>
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{Entity}" />
    public class EntityCollection
      : ObservableCollection<Entity>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// Get <see cref="Entity" /> with the specified table <paramref name="fullName"/>.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns>The <see cref="Entity"/> with the specified table <paramref name="fullName"/>. </returns>
        public Entity ByTable(string fullName)
        {
            return this.FirstOrDefault(x => x.FullName == fullName);
        }

        /// <summary>
        /// Get <see cref="Entity" /> with the specified table <paramref name="tableName" /> and <paramref name="tableSchema" />.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="tableSchema">The table schema.</param>
        /// <returns>
        /// The <see cref="Entity" /> with the specified table <paramref name="tableName" /> and <paramref name="tableSchema" />.
        /// </returns>
        public Entity ByTable(string tableName, string tableSchema)
        {
            return this.FirstOrDefault(x => x.TableName == tableName && x.TableSchema == tableSchema);
        }

        /// <summary>
        /// Get <see cref="Entity" /> with the specified <paramref name="className" />.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <returns>
        /// The <see cref="Entity" /> with the specified <paramref name="className" />.
        /// </returns>
        public Entity ByClass(string className)
        {
            return this.FirstOrDefault(x => x.EntityClass == className);
        }
    }
}
