namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Model options group
/// </summary>
public class ViewModel
{
    /// <summary>
    /// Gets or sets the shared options between read,create and update models
    /// </summary>
    /// <value>
    /// The shared options between read,create and update models.
    /// </value>
    public SharedModel Shared { get; set; } = new();

    /// <summary>
    /// Gets or sets the read model options.
    /// </summary>
    /// <value>
    /// The read model options.
    /// </value>
    public ReadModel Read { get; set; } = new();

    /// <summary>
    /// Gets or sets the create model options.
    /// </summary>
    /// <value>
    /// The create model options.
    /// </value>
    public CreateModel Create { get; set; } = new();

    /// <summary>
    /// Gets or sets the update model options.
    /// </summary>
    /// <value>
    /// The update model options.
    /// </value>
    public UpdateModel Update { get; set; } = new();

    /// <summary>
    /// Gets or sets the view model mapper options.
    /// </summary>
    /// <value>
    /// The view model mapper options.
    /// </value>
    public MapperClass Mapper { get; set; } = new();

    /// <summary>
    /// Gets or sets the model validator options.
    /// </summary>
    /// <value>
    /// The model validator options.
    /// </value>
    public ValidatorClass Validator { get; set; } = new();

}
