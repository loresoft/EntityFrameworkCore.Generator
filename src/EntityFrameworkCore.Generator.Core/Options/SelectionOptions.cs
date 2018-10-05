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
            Entities = new List<string>();
            Properties = new List<string>();
        }

        /// <summary>
        /// Gets or sets a list of regular expression of entities to select.
        /// </summary>
        /// <value>
        /// The list of regular expression of entities to select.
        /// </value>
        public List<string> Entities { get; set; }


        /// <summary>
        /// Gets or sets a list of regular expression of properties to select.
        /// </summary>
        /// <value>
        /// The list of regular expression of properties to select.
        /// </value>
        public List<string> Properties { get; set; }

    }
}