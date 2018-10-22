using System.Collections.Generic;
using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Database options for reverse engineering the database
    /// </summary>
    public class DatabaseOptions : OptionsBase
    {
        public DatabaseOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Database"))
        {
            Provider = DatabaseProviders.SqlServer;
            TableNaming = TableNaming.Singular;
            Tables = new List<string>();
            Schemas = new List<string>();
        }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string Name
        {
            get => GetProperty();
            set => SetProperty(value);
        }


        /// <summary>
        /// Gets or sets the database to generate code for.
        /// </summary>
        /// <value>
        /// The database to generate code for.
        /// </value>
        [DefaultValue(DatabaseProviders.SqlServer)]
        public DatabaseProviders Provider { get; set; }


        /// <summary>
        /// Gets or sets the connection string for reverse engineering the database
        /// </summary>
        /// <value>
        /// The connection string for reverse engineering the database
        /// </value>
        public string ConnectionString
        {
            get => GetProperty();
            set => SetProperty(value);
        }



        /// <summary>
        /// Gets or sets the table naming hint for how existing tables are named. This is used to determine if the name should be converted.
        /// </summary>
        /// <value>
        /// The table naming hint for how tables are named.
        /// </value>
        [DefaultValue(TableNaming.Singular)]
        public TableNaming TableNaming { get; set; }



        /// <summary>
        /// Gets or sets the tables to include in the model, or an empty enumerable to include all
        /// </summary>
        /// <value>
        /// The tables to include in the model, or an empty enumerable to include all
        /// </value>
        public List<string> Tables { get; set; }

        /// <summary>
        /// Gets or sets the schema to include in the model, or an empty enumerable to include all.
        /// </summary>
        /// <value>
        /// The schema to include in the model, or an empty enumerable to include all.
        /// </value>
        public List<string> Schemas { get; set; }
    }
}
