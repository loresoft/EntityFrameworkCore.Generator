using System;

namespace EntityFrameworkCore.Generator.Options
{

    /// <summary>
    /// Project options
    /// </summary>
    public class ProjectOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectOptions"/> class.
        /// </summary>
        public ProjectOptions()
        {
            Namespace = "{Database.Name}";
            Directory = @".\";
        }

        /// <summary>
        /// Gets or sets the project root namespace.
        /// </summary>
        /// <value>
        /// The project root namespace.
        /// </value>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the project directory.
        /// </summary>
        /// <value>
        /// The project directory.
        /// </value>
        public string Directory { get; set; }
    }
}