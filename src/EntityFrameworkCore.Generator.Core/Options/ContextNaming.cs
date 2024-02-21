using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Naming strategies for entity data set property on the generated <see cref="DbContext"/>
/// </summary>
public enum ContextNaming
{
    /// <summary>
    /// Preserve the entity name as is
    /// </summary>
    Preserve = 0,

    /// <summary>
    /// Convert the entity to plural form
    /// </summary>
    Plural = 1,

    /// <summary>
    /// Add 'DataSet' to the end of the entity name.
    /// </summary>
    Suffix = 2
}
