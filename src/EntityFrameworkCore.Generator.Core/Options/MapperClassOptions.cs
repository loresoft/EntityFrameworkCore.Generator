namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// View model mapper class options
/// </summary>
/// <seealso cref="EntityFrameworkCore.Generator.Options.ClassOptionsBase" />
public class MapperClassOptions : ClassOptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapperClassOptions"/> class.
    /// </summary>
    public MapperClassOptions(VariableDictionary variables, string? prefix)
        : base(variables, AppendPrefix(prefix, "Mapper"))
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
}
