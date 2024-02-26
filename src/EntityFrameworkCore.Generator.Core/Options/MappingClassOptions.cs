using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// EntityFramework mapping class generation options
/// </summary>
/// <seealso cref="ClassOptionsBase" />
public class MappingClassOptions : ClassOptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingClassOptions"/> class.
    /// </summary>
    public MappingClassOptions(VariableDictionary variables, string prefix)
        : base(variables, AppendPrefix(prefix, "Mapping"))
    {
        Namespace = "{Project.Namespace}.Data.Mapping";
        Directory = @"{Project.Directory}\Data\Mapping";
        Name = "{Entity.Name}Map";
    }

    /// <summary>
    /// Gets or sets if temporal table mapping is enabled. Default true
    /// </summary>
    /// <value>
    /// If temporal table mapping is enabled.
    /// </value>
    [DefaultValue(true)]
    public bool Temporal { get; set; } = true;

    /// <summary>
    /// Gets or sets how row version properties should be mapped.
    /// </summary>
    /// <value>
    /// How row version properties should be mapped.
    /// </value>
    /// <seealso cref="RowVersionMapping"/>
    [DefaultValue(RowVersionMapping.ByteArray)]
    public RowVersionMapping RowVersion { get; set; } = RowVersionMapping.ByteArray;

}
