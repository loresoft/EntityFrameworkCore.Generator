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
        SystemTypeAnnotation = "Generator:SystemType";
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
    public SelectionModel? Renaming { get; set; }

    /// <summary>
    /// Gets or sets if mapping attributes are included on the entity classes. Default false
    /// </summary>
    /// <value>
    /// If mapping attributes are included.
    /// </value>
    [DefaultValue(false)]
    public bool MappingAttributes { get; set; }

    /// <summary>
    /// Gets or sets the native type to system type mappings.
    /// </summary>
    public List<TypeMapping>? TypeMapping { get; set; }

    /// <summary>
    /// Gets or sets the column annotation name used to override generated .NET system type names.
    /// </summary>
    public string? SystemTypeAnnotation { get; set; }

    /// <summary>
    /// Additional using statements, ;-separated - example: MyLib;MyLib.Data;MyLib.Domain;MyLib.Domain.Interfaces
    /// </summary>
    public string AdditionalUsings { get; set; }
}
