using System;

namespace EntityFrameworkCore.Generator.Metadata.Generation
{

    /// <summary>
    /// A base class for entity generation models
    /// </summary>
    public class ModelBase
    {

        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessed { get; set; }
    }
}
