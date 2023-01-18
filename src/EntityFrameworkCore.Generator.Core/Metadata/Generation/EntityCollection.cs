using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

/// <summary>
/// A collection of <see cref="Entity"/>
/// </summary>
public class EntityCollection
    : List<Entity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityCollection"/> class.
    /// </summary>
    public EntityCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityCollection"/> class.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public EntityCollection(IEnumerable<Entity> collection) : base(collection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityCollection"/> class.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public EntityCollection(int capacity) : base(capacity)
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