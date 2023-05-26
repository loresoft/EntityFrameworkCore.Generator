namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// Create model file generation options
/// </summary>
public class CreateModel : ModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateModel"/> class.
    /// </summary>
    public CreateModel()
    {
        Name = "{Entity.Name}CreateModel";
    }
}
