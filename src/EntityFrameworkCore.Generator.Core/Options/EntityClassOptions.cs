using System.ComponentModel;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// EntityFramework entity class generation options
/// </summary>
/// <seealso cref="ClassOptionsBase" />
public class EntityClassOptions : ClassOptionsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityClassOptions"/> class.
    /// </summary>
    public EntityClassOptions(VariableDictionary variables, string prefix)
        : base(variables, AppendPrefix(prefix, "Entity"))
    {
        Namespace = "{Project.Namespace}.Data.Entities";
        Directory = @"{Project.Directory}\Data\Entities";

        RelationshipNaming = RelationshipNaming.Plural;
        EntityNaming = EntityNaming.Singular;
        PrefixWithSchemaName = false;

        Renaming = new SelectionOptions(variables, AppendPrefix(prefix, "Naming"));
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
    public SelectionOptions Renaming { get; }

    /// <summary>
    /// Gets or sets if mapping attributes are included on the entity classes. Default false
    /// </summary>
    /// <value>
    /// If mapping attributes are included.
    /// </value>
    [DefaultValue(false)]
    public bool MappingAttributes { get; set; }
}
