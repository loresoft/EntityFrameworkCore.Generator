using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Database options for reverse engineering the database
    /// </summary>
    public class DatabaseOptions
    {

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the database to generate code for.
        /// </summary>
        /// <value>
        /// The database to generate code for.
        /// </value>
        [DefaultValue(DatabaseProviders.SqlServer)]
        public DatabaseProviders Provider { get; set; } = DatabaseProviders.SqlServer;

        /// <summary>
        /// Gets or sets the connection string for reverse engineering the database
        /// </summary>
        /// <value>
        /// The connection string for reverse engineering the database
        /// </value>
        public string ConnectionString { get; set; }


        /// <summary>
        /// Gets or sets the table naming hint for how existing tables are named. This is used to determine if the name should be converted.
        /// </summary>
        /// <value>
        /// The table naming hint for how tables are named.
        /// </value>
        [DefaultValue(TableNaming.Singular)]
        public TableNaming TableNaming { get; set; } = TableNaming.Singular;
    }
}
