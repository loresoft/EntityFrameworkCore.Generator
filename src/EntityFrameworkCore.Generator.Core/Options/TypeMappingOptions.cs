namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Maps a native database type to a generated .NET system type.
/// </summary>
public class TypeMappingOptions
{
    /// <summary>
    /// Gets or sets the native database type name.
    /// </summary>
    public string NativeType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the generated .NET system type name.
    /// </summary>
    public string SystemType { get; set; } = null!;
}
