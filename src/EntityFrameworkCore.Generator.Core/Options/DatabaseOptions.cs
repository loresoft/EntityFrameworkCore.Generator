// Ignore Spelling: Schemas

using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Database options for reverse engineering the database
/// </summary>
public class DatabaseOptions : OptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseOptions"/> class.
    /// </summary>
    /// <param name="variables">The shared variables dictionary.</param>
    /// <param name="prefix">The variable key prefix.</param>
    public DatabaseOptions(VariableDictionary variables, string? prefix)
        : base(variables, AppendPrefix(prefix, "Database"))
    {
        Provider = DatabaseProviders.SqlServer;
        TableNaming = TableNaming.Singular;
        Tables = [];
        Schemas = [];
        Exclude = new DatabaseMatchOptions(Variables, Prefix);
    }

    /// <summary>
    /// Gets or sets the name of the database.
    /// </summary>
    /// <value>
    /// The name of the database.
    /// </value>
    public string? Name
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
    public string? ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the name of the connection in the user secret file.
    /// </summary>
    /// <value>
    /// The name of the connection.
    /// </value>
    public string? ConnectionName { get; set; }

    /// <summary>
    /// Gets or sets the user secrets identifier. A user secrets ID is unique value used to store and identify a collection of secret configuration values.
    /// </summary>
    /// <value>
    /// The user secrets identifier.
    /// </value>
    public string? UserSecretsId { get; set; }


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
    public List<string> Tables { get; }

    /// <summary>
    /// Gets or sets the schema to include in the model, or an empty enumerable to include all.
    /// </summary>
    /// <value>
    /// The schema to include in the model, or an empty enumerable to include all.
    /// </value>
    public List<string> Schemas { get; }

    /// <summary>
    /// Gets or sets the exclude table options.
    /// </summary>
    /// <value>
    /// The exclude table options.
    /// </value>
    public DatabaseMatchOptions Exclude { get; }

}
