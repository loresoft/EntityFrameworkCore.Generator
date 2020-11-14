using EntityFrameworkCore.Generator.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.Generator.Scripts
{
    public abstract class ScriptVariablesBase
    {
        protected ScriptVariablesBase(GeneratorOptions generatorOptions, TemplateOptions templateOptions)
        {
            GeneratorOptions = generatorOptions;
            TemplateOptions = templateOptions;
            CodeBuilder = new IndentedStringBuilder();
        }

        public TemplateOptions TemplateOptions { get; }

        public GeneratorOptions GeneratorOptions { get; }

        public IndentedStringBuilder CodeBuilder { get; }
    }
}