using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

/// <summary>
/// A collection of <see cref="Model"/>
/// </summary>
public class ModelCollection
    : List<Model>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelCollection"/> class.
    /// </summary>
    public ModelCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelCollection"/> class.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public ModelCollection(IEnumerable<Model> collection) : base(collection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelCollection"/> class.
    /// </summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public ModelCollection(int capacity) : base(capacity)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is processed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is processed; otherwise, <c>false</c>.
    /// </value>
    public bool IsProcessed { get; set; }
}