namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// View model mapper class options
/// </summary>
public class MapperClass : ClassBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapperClass"/> class.
    /// </summary>
    public MapperClass()
    {
        Generate = false;
        Namespace = "{Project.Namespace}.Domain.Mapping";
        Directory = @"{Project.Directory}\Domain\Mapping";

        BaseClass = "AutoMapper.Profile";
        Name = "{Entity.Name}Profile";
    }

    /// <summary>
    /// Gets or sets a value indicating whether this option is generated.
    /// </summary>
    /// <value>
    ///   <c>true</c> to generate; otherwise, <c>false</c>.
    /// </value>
    public bool Generate { get; set; }

    /// <summary>
    /// Gets or sets the mapper class name template.
    /// </summary>
    /// <value>
    /// The mapper class name template.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the base class to inherit from.
    /// </summary>
    /// <value>
    /// The base class.
    /// </value>
    public string BaseClass { get; set; }
}
