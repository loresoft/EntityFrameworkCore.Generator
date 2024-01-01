namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// How row versions should be mapped
/// </summary>
public enum RowVersionMapping
{
    /// <summary>
    /// Map as byte array, default
    /// </summary>
    ByteArray = 0,
    /// <summary>
    /// Map as a long
    /// </summary>
    Long = 1,
    /// <summary>
    /// Map as a ulong
    /// </summary>
    ULong = 2,
}
