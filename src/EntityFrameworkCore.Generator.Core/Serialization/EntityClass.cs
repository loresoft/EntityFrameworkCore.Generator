using System.ComponentModel;

using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Serialization;

/// <summary>
/// EntityFramework entity class generation options
/// </summary>
/// <seealso cref="ClassBase" />
public class EntityClass : ClassBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityClassOptions"/> class.
    /// </summary>
    public EntityClass()
    {
        Namespace = "{Project.Namespace}.Data.Entities";
        Directory = @"{Project.Directory}\Data\Entities";

        RelationshipNaming = RelationshipNaming.Plural;
        EntityNaming = EntityNaming.Singular;
        PrefixWithSchemaName = false;
    }

    /// <summary>
    /// Gets or sets the entity class naming strategy.
    /// </summary>
    /// <value>
    /// The entity class naming strategy.
    /// </value>
    [DefaultValue(EntityNaming.Singular)]
    public EntityNaming EntityNaming { get; set; }

    /// <summary>
    /// Gets or sets the relationship property naming strategy.
    /// </summary>
    /// <value>
    /// The relationship property naming strategy.
    /// </value>
    [DefaultValue(RelationshipNaming.Plural)]
    public RelationshipNaming RelationshipNaming { get; set; }

    /// <summary>
    /// If true prefix classname with schema name to prevent naming conflicts
    /// </summary>
    [DefaultValue(false)]
    public bool PrefixWithSchemaName { get; set; }

    /// <summary>
    /// Gets or sets the renaming expressions.
    /// </summary>
    /// <value>
    /// The renaming expressions.
    /// </value>
    public SelectionModel Renaming { get; set; }

    /// <summary>
    /// If true, files without a corresponding database table will be removed in the folder
    /// </summary>
    [DefaultValue(false)]
    public bool DeleteUnusedFiles { get; set; }
}
