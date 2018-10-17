using System;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Top level generator configuration options
    /// </summary>
    public class GeneratorOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorOptions"/> class.
        /// </summary>
        public GeneratorOptions()
        {
            Project = new ProjectOptions();
            Database = new DatabaseOptions();
            Data = new DataOptions();
            Model = new ModelOptions();
        }

        /// <summary>
        /// Gets or sets the project options.
        /// </summary>
        /// <value>
        /// The project level options.
        /// </value>
        public ProjectOptions Project { get; set; }

        /// <summary>
        /// Gets or sets the options for reverse engineer the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public DatabaseOptions Database { get; set; }

        /// <summary>
        /// Gets or sets the EntityFramework configuration options.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public DataOptions Data { get; set; }

        /// <summary>
        /// Gets or sets the domain view model options.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public ModelOptions Model { get; set; }
    }
}