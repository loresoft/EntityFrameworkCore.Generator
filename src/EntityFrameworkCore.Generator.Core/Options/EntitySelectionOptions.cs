using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Selection options
    /// </summary>
    public class EntitySelectionOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySelectionOptions"/> class.
        /// </summary>
        public EntitySelectionOptions()
        {
            Columns = new List<string>();
        }

        /// <summary>
        /// Gets or sets a list of regular expression of columns to select.
        /// </summary>
        /// <value>
        /// The list of regular expression of columns to select.
        /// </value>
        public List<string> Columns { get; set; }
    }
}