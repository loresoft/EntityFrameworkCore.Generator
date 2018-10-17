using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{
    /// <summary>
    /// A collection of <see cref="Method"/> instances
    /// </summary>
    public class MethodCollection
        : List<Method>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCollection"/> class.
        /// </summary>
        public MethodCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        public MethodCollection(IEnumerable<Method> collection) : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCollection"/> class.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public MethodCollection(int capacity) : base(capacity)
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
}
