using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Scripts;

public class ContextScriptTemplate : ScriptTemplateBase<ContextScriptVariables>
{
    private EntityContext _entityContext;

    public ContextScriptTemplate(ILoggerFactory loggerFactory, GeneratorOptions generatorOptions, TemplateOptions templateOptions)
        : base(loggerFactory, generatorOptions, templateOptions)
    {
    }

    public void RunScript(EntityContext entityContext)
    {
        _entityContext = entityContext;

        WriteCode();
    }

    protected override ContextScriptVariables CreateVariables()
    {
        return new ContextScriptVariables(_entityContext, GeneratorOptions, TemplateOptions);
    }
}