namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Generator Options
/// </summary>
public class GeneratorModel
{
    /// <summary>
    /// Gets or sets the project .
    /// </summary>
    /// <value>
    /// The project level .
    /// </value>
    public ProjectModel Project { get; set; } = new();

    /// <summary>
    /// Gets or sets the  for reverse engineer the database.
    /// </summary>
    /// <value>
    /// The database.
    /// </value>
    public DatabaseModel Database { get; set; } = new();

    /// <summary>
    /// Gets or sets the EntityFramework configuration .
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public DataModel Data { get; set; } = new();

    /// <summary>
    /// Gets or sets the domain view model .
    /// </summary>
    /// <value>
    /// The model.
    /// </value>
    public ViewModel Model { get; set; } = new();

    /// <summary>
    /// Gets or sets the script template .
    /// </summary>
    /// <value>
    /// The script template .
    /// </value>
    public ScriptModel? Script { get; set; }
}
