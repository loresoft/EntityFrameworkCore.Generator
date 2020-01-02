using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Scripts
{
    public class EntityScriptTemplate : ScriptTemplateBase<EntityScriptVariables>
    {
        private Entity _entity;

        public EntityScriptTemplate(ILoggerFactory loggerFactory, GeneratorOptions generatorOptions, TemplateOptions templateOptions) 
            : base(loggerFactory, generatorOptions, templateOptions)
        {
        }

        public void RunScript(Entity entity)
        {
            _entity = entity;

            WriteCode();
        }

        protected override EntityScriptVariables CreateVariables()
        {
            return new EntityScriptVariables(_entity, GeneratorOptions, TemplateOptions);
        }
    }
}