using System.ComponentModel;

using EntityFrameworkCore.Generator.Options;

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

    /// <summary>
    /// If true, files without a corresponding database table will be removed in the folder
    /// </summary>
    [DefaultValue(false)]
    public bool DeleteUnusedFiles { get; set; }

}
