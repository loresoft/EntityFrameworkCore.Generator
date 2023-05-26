namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// EntityFramework mapping class generation options
/// </summary>
public class MappingClass : ClassBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingClass"/> class.
    /// </summary>
    public MappingClass()
    {
        Namespace = "{Project.Namespace}.Data.Mapping";
        Directory = @"{Project.Directory}\Data\Mapping";
    }
}
