using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Scripts;

public class EntityScriptVariables : ScriptVariablesBase
{
    public EntityScriptVariables(Entity entity, GeneratorOptions generatorOptions, TemplateOptions templateOptions)
        : base(generatorOptions, templateOptions)
    {
        Entity = entity;
    }

    public Entity Entity { get; }
}