using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Scripts;

public class ModelScriptVariables : ScriptVariablesBase
{
    public ModelScriptVariables(Model model, GeneratorOptions generatorOptions, TemplateOptions templateOptions)
        : base(generatorOptions, templateOptions)
    {
        Model = model;
    }

    public Model Model { get; }
}