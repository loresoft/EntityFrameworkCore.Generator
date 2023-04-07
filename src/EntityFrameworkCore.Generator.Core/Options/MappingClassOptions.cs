using System.ComponentModel;

using Microsoft.EntityFrameworkCore;

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
        RelationshipDeleteBehavior = DeleteBehavior.Cascade;
    }

    /// <summary>
    /// Gets or sets the delete behavior for all foreign keys that have cascade deletes.
    /// </summary>
    [DefaultValue(DeleteBehavior.Cascade)]
    public DeleteBehavior RelationshipDeleteBehavior { get; set; }
}
