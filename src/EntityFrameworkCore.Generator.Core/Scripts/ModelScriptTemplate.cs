using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Scripts
{
    public class ModelScriptTemplate : ScriptTemplateBase<ModelScriptVariables>
    {
        private Model _model;

        public ModelScriptTemplate(ILoggerFactory loggerFactory, GeneratorOptions generatorOptions, TemplateOptions templateOptions)
            : base(loggerFactory, generatorOptions, templateOptions)
        {
        }

        public void RunScript(Model model)
        {
            _model = model;

            WriteCode();
        }

        protected override ModelScriptVariables CreateVariables()
        {
            return new ModelScriptVariables(_model, GeneratorOptions, TemplateOptions);
        }
    }
}