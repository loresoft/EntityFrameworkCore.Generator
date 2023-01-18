using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Scripts;

public class ContextScriptVariables : ScriptVariablesBase
{
    public ContextScriptVariables(EntityContext entityContext, GeneratorOptions generatorOptions, TemplateOptions templateOptions)
        : base(generatorOptions, templateOptions)
    {
        EntityContext = entityContext;
    }

    public EntityContext EntityContext { get; }
}