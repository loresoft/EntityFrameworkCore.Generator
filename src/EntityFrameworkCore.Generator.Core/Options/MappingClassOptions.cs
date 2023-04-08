using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Options;

/// <summary>
/// EntityFramework mapping class generation options
/// </summary>
/// <seealso cref="ClassOptionsBase" />
public class MappingClassOptions : ClassOptionsBase
{
    static List<DeleteBehavior> _validCascadeBehaviors = new List<DeleteBehavior> { DeleteBehavior.Cascade, DeleteBehavior.ClientCascade };
    static List<DeleteBehavior> _validSetNullBehaviors = new List<DeleteBehavior> { DeleteBehavior.SetNull, DeleteBehavior.ClientSetNull };
    static List<DeleteBehavior> _validNoActionBehaviors = new List<DeleteBehavior> { DeleteBehavior.NoAction, DeleteBehavior.ClientNoAction };

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

    public override bool Validate(ILogger logger)
    {
        var errors = new List<string>();
        if (_validCascadeBehaviors.Any(a => a == GlobalRelationshipCascadeDeleteBehavior) == false)
        {
            errors.Add(GetBehaviorValidationError(nameof(GlobalRelationshipCascadeDeleteBehavior), GlobalRelationshipCascadeDeleteBehavior, _validCascadeBehaviors));
        }
        if (_validSetNullBehaviors.Any(a => a == GlobalRelationshipSetNullDeleteBehavior) == false)
        {
            errors.Add(GetBehaviorValidationError(nameof(GlobalRelationshipSetNullDeleteBehavior), GlobalRelationshipSetNullDeleteBehavior, _validSetNullBehaviors));
        }
        if (_validNoActionBehaviors.Any(a => a == GlobalRelationshipNoActionDeleteBehavior) == false)
        {
            errors.Add(GetBehaviorValidationError(nameof(GlobalRelationshipNoActionDeleteBehavior), GlobalRelationshipNoActionDeleteBehavior, _validNoActionBehaviors));
        }

        if (errors.Any())
        {
            errors.ForEach(err =>
            {
                logger.LogError(err);
            });
            throw new InvalidEnumArgumentException(errors.FirstOrDefault());
        }

        // always call the base
        return base.Validate(logger);

        string GetBehaviorValidationError(string propertyName, DeleteBehavior behavior, List<DeleteBehavior> behaviors)
        {
            var error = $"{propertyName} can only be set to {string.Join(" or ", behaviors)}. Received {behavior}";
            return error;
        }
    }

    /// <summary>
    /// Gets or sets the delete behavior globally for all relationships who's foreign keys have a insert delete <i>Cascade</i> rule set.
    /// </summary>
    [DefaultValue(DeleteBehavior.Cascade)]
    public DeleteBehavior GlobalRelationshipCascadeDeleteBehavior { get; set; }

    /// <summary>
    /// Gets or sets the delete behavior globally for all relationships who's foreign keys have a insert delete <i>Set Null</i> rule set.
    /// </summary>
    [DefaultValue(DeleteBehavior.SetNull)]
    public DeleteBehavior GlobalRelationshipSetNullDeleteBehavior { get; set; }

    /// <summary>
    /// Gets or sets the delete behavior globally for all relationships who's foreign keys have a insert delete <i>No Action</i> rule set.
    /// </summary>
    [DefaultValue(DeleteBehavior.NoAction)]
    public DeleteBehavior GlobalRelationshipNoActionDeleteBehavior { get; set; }
}
