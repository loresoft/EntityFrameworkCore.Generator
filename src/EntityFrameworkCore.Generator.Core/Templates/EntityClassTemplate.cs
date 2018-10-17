using System.Linq;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using Microsoft.EntityFrameworkCore.Internal;

namespace EntityFrameworkCore.Generator.Templates
{
    public class EntityClassTemplate : CodeTemplateBase
    {
        private readonly Entity _entity;

        public EntityClassTemplate(Entity entity)
        {
            _entity = entity;
        }

        public override string WriteCode()
        {
            CodeBuilder.Clear();

            CodeBuilder.AppendLine("using System;");
            CodeBuilder.AppendLine("using System.Collections.Generic;");
            CodeBuilder.AppendLine();

            CodeBuilder.AppendLine($"namespace {_entity.EntityNamespace}");
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
            var entityClass = _entity.EntityClass.ToSafeName();

            CodeBuilder.AppendLine($"public partial class {entityClass}");
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateConstructor();
                GenerateProperties();
                GenerateRelationshipProperties();
            }

            CodeBuilder.AppendLine("}");

        }

        private void GenerateConstructor()
        {
            var relationships = _entity.Relationships
                .Where(r => r.Cardinality == Cardinality.Many)
                .ToList();

            CodeBuilder.AppendLine($"public {_entity.EntityClass.ToSafeName()}()");
            if (_entity.EntityBaseClass.HasValue())
                using (CodeBuilder.Indent())
                    CodeBuilder.AppendLine($": {_entity.EntityBaseClass}");

            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                CodeBuilder.AppendLine("#region Generated Initializes");
                foreach (var relationship in relationships)
                {
                    var propertyName = relationship.PropertyName.ToSafeName();
                    var primaryName = relationship.PrimaryEntity.EntityClass.ToSafeName();

                    CodeBuilder.AppendLine($"{propertyName} = new HashSet<{primaryName}>();");
                }
                CodeBuilder.AppendLine("#endregion");
            }

            CodeBuilder.AppendLine("}");
            CodeBuilder.AppendLine();
        }

        private void GenerateProperties()
        {
            CodeBuilder.AppendLine("#region Generated Properties");
            foreach (var property in _entity.Properties)
            {
                var propertyType = property.SystemType.ToNullableType(property.IsNullable == true);
                var propertyName = property.PropertyName.ToSafeName();

                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }}");
            }
            CodeBuilder.AppendLine("#endregion");
        }

        private void GenerateRelationshipProperties()
        {
            CodeBuilder.AppendLine("#region Generated Relationships");
            foreach (var relationship in _entity.Relationships)
            {
                var propertyName = relationship.PropertyName.ToSafeName();
                var primaryName = relationship.PrimaryEntity.EntityClass.ToSafeName();

                if (relationship.Cardinality == Cardinality.Many)
                    CodeBuilder.AppendLine($"public virtual ICollection<{primaryName}> {propertyName} {{ get; set; }}");
                else
                    CodeBuilder.AppendLine($"public virtual {primaryName} {propertyName} {{ get; set; }}");
            }
            CodeBuilder.AppendLine("#endregion");
        }
    }
}
