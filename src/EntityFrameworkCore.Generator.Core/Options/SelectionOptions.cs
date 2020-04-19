using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Selection options
    /// </summary>
    public class SelectionOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionOptions"/> class.
        /// </summary>
        public SelectionOptions()
        {
            Entities = new List<MatchOptions>();
            Properties = new List<MatchOptions>();
        }

        /// <summary>
        /// Gets or sets a list of regular expression of entities to select.
        /// </summary>
        /// <value>
        /// The list of regular expression of entities to select.
        /// </value>
        public List<MatchOptions> Entities { get; set; }
        
        /// <summary>
        /// Gets or sets a list of regular expression of properties to select.
        /// </summary>
        /// <value>
        /// The list of regular expression of properties to select.
        /// </value>
        public List<MatchOptions> Properties { get; set; }
    }
}