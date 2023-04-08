using System.ComponentModel;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// EntityFramework mapping class generation options
/// </summary>
/// <seealso cref="ClassOptionsBase" />
public class MappingClassOptions : ClassOptionsBase
{
    DeleteBehavior _globalRelationshipCascadeDeleteBehavior;
    DeleteBehavior _globalRelationshipSetNullDeleteBehavior;
    DeleteBehavior _globalRelationshipNoActionDeleteBehavior;

    /// <summary>
    /// Initializes a new instance of the <see cref="MappingClassOptions"/> class.
    /// </summary>
    public MappingClassOptions(VariableDictionary variables, string prefix)
        : base(variables, AppendPrefix(prefix, "Mapping"))
    {
        Namespace = "{Project.Namespace}.Data.Mapping";
        Directory = @"{Project.Directory}\Data\Mapping";
        GlobalRelationshipCascadeDeleteBehavior = DeleteBehavior.Cascade;
        GlobalRelationshipSetNullDeleteBehavior = DeleteBehavior.SetNull;
        GlobalRelationshipNoActionDeleteBehavior = DeleteBehavior.NoAction;
    }

    /// <summary>
    /// Gets or sets the delete behavior globally for all relationships who's foreign keys have a insert delete <i>Cascade</i> rule set.
    /// </summary>
    [DefaultValue(DeleteBehavior.Cascade)]
    public DeleteBehavior GlobalRelationshipCascadeDeleteBehavior
    {
        get => _globalRelationshipCascadeDeleteBehavior;
        set
        {
            if (value == DeleteBehavior.Cascade || value == DeleteBehavior.ClientCascade)
            {
                _globalRelationshipCascadeDeleteBehavior = value;
            }
            else
            {
                throw new InvalidEnumArgumentException($"{nameof(GlobalRelationshipCascadeDeleteBehavior)} can only be set to {nameof(DeleteBehavior.Cascade)} or {nameof(DeleteBehavior.ClientCascade)}. Received {value}");
            }
        }
    }

    /// <summary>
    /// Gets or sets the delete behavior globally for all relationships who's foreign keys have a insert delete <i>Set Null</i> rule set.
    /// </summary>
    [DefaultValue(DeleteBehavior.SetNull)]
    public DeleteBehavior GlobalRelationshipSetNullDeleteBehavior
    {
        get => _globalRelationshipSetNullDeleteBehavior;
        set
        {
            if (value == DeleteBehavior.SetNull || value == DeleteBehavior.ClientSetNull)
            {
                _globalRelationshipSetNullDeleteBehavior = value;
            }
            else
            {
                throw new InvalidEnumArgumentException($"{nameof(GlobalRelationshipSetNullDeleteBehavior)} can only be set to {nameof(DeleteBehavior.SetNull)} or {nameof(DeleteBehavior.ClientSetNull)}. Received {value}");
            }
        }
    }

    /// <summary>
    /// Gets or sets the delete behavior globally for all relationships who's foreign keys have a insert delete <i>No Action</i> rule set.
    /// </summary>
    [DefaultValue(DeleteBehavior.NoAction)]
    public DeleteBehavior GlobalRelationshipNoActionDeleteBehavior
    {
        get => _globalRelationshipNoActionDeleteBehavior;
        set
        {
            if (value != DeleteBehavior.NoAction || value != DeleteBehavior.ClientNoAction)
            {
                _globalRelationshipNoActionDeleteBehavior = value;
            }
            else
            {
                throw new InvalidEnumArgumentException($"{nameof(GlobalRelationshipNoActionDeleteBehavior)} can only be set to {nameof(DeleteBehavior.NoAction)} or {nameof(DeleteBehavior.ClientNoAction)}. Received {value}");
            }
        }
    }
}
