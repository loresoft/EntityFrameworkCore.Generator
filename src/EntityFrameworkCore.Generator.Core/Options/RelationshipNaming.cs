namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// Relationship property naming strategy
/// </summary>
public enum RelationshipNaming
{
    /// <summary>
    /// Preserve property name as the entity name
    /// </summary>
    Preserve = 0,

    /// <summary>
    /// Convert the property name to the entity plural name
    /// </summary>
    Plural = 1,

    /// <summary>
    /// Add 'List' to the end of the entity name.
    /// </summary>
    Suffix = 2
}