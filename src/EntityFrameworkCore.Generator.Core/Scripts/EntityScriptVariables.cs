using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Scripts;

public class EntityScriptVariables : ScriptVariablesBase
{
    public EntityScriptVariables(Entity entity, GeneratorOptions generatorOptions, TemplateOptions templateOptions)
        : base(generatorOptions, templateOptions)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public Entity Entity { get; }
}
