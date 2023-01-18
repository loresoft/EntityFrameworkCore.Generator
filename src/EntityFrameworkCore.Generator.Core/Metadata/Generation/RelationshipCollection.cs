using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

/// <summary>
/// A collection of <see cref="Relationship"/> instances
/// </summary>
public class RelationshipCollection
    : List<Relationship>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RelationshipCollection"/> class.
    /// </summary>
    public RelationshipCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RelationshipCollection"/> class.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public RelationshipCollection(IEnumerable<Relationship> collection) : base(collection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RelationshipCollection"/> class.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public RelationshipCollection(int capacity) : base(capacity)
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
    /// Gets a relationship by name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public Relationship ByName(string name)
    {
        return this.FirstOrDefault(x => x.RelationshipName == name);
    }

    /// <summary>
    /// Gets a relationship by the property name.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public Relationship ByProperty(string propertyName)
    {
        return this.FirstOrDefault(x => x.PropertyName == propertyName);
    }
}