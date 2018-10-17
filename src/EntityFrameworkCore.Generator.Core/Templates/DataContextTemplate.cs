using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using Microsoft.EntityFrameworkCore.Internal;

namespace EntityFrameworkCore.Generator.Templates
{
    public class DataContextTemplate : CodeTemplateBase
    {
        private readonly EntityContext _entityContext;

        public DataContextTemplate(EntityContext entityContext)  
        {
            _entityContext = entityContext;
        }
        
        public override string WriteCode()
        {
            CodeBuilder.Clear();

            CodeBuilder.AppendLine("using System;");
            CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
            CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore.Metadata;");
            CodeBuilder.AppendLine();

            CodeBuilder.AppendLine($"namespace {_entityContext.ContextNamespace}");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateClass();
            }

            CodeBuilder.AppendLine("}");

            return CodeBuilder.ToString();
        }


        private void GenerateClass()
        {
            var contextClass = _entityContext.ContextClass.ToSafeName();
            var baseClass = _entityContext.ContextBaseClass.ToSafeName();

            CodeBuilder.AppendLine($"public partial class {contextClass} : {baseClass}");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateConstructors();
                GenerateDbSets();
                GenerateOnConfiguring();
            }

            CodeBuilder.AppendLine("}");
        }

        private void GenerateConstructors()
        {
            var contextName = _entityContext.ContextClass.ToSafeName();

            CodeBuilder.AppendLine($"public {contextName}()")
                .AppendLine("{")
                .AppendLine("}")
                .AppendLine();

            CodeBuilder.AppendLine($"public {contextName}(DbContextOptions<{contextName}> options)")
                .IncrementIndent()
                .AppendLine(": base(options)")
                .DecrementIndent()
                .AppendLine("{")
                .AppendLine("}")
                .AppendLine();
        }

        private void GenerateDbSets()
        {
            CodeBuilder.AppendLine("#region Generated Properties");
            foreach (var entityType in _entityContext.Entities)
            {
                var entityClass = entityType.EntityClass.ToSafeName();
                var propertyName = entityType.ContextProperty.ToSafeName();

                CodeBuilder.AppendLine($"public virtual DbSet<{entityType.EntityNamespace}.{entityClass}> {propertyName} {{ get; set; }}");
            }

            CodeBuilder.AppendLine("#endregion");

            if (_entityContext.Entities.Any())
                CodeBuilder.AppendLine();
        }

        private void GenerateOnConfiguring()
        {
            CodeBuilder.AppendLine("protected override void OnModelCreating(ModelBuilder modelBuilder)");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                CodeBuilder.AppendLine("#region Generated Configuration");
                foreach (var entityType in _entityContext.Entities)
                {
                    var mappingClass = entityType.MappingClass.ToSafeName();

                    CodeBuilder.AppendLine($"modelBuilder.ApplyConfiguration(new {entityType.MappingNamespace}.{mappingClass}());");
                }

                CodeBuilder.AppendLine("#endregion");
            }

            CodeBuilder.AppendLine("}");
        }
    }
}
