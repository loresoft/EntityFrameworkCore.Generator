using System.Linq;

using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.Generator.Templates;

public class EntityClassTemplate : CodeTemplateBase
{
    private readonly Entity _entity;

    public EntityClassTemplate(Entity entity, GeneratorOptions options) : base(options)
    {
        _entity = entity;
    }

    public override string WriteCode()
    {
        CodeBuilder.Clear();

        if (Options.Data.Entity.Header.HasValue())
            CodeBuilder.AppendLine(Options.Data.Entity.Header).AppendLine();

        CodeBuilder.AppendLine("using System;");
        CodeBuilder.AppendLine("using System.Collections.Generic;");
        CodeBuilder.AppendLine();

        CodeBuilder.Append($"namespace {_entity.EntityNamespace}");

        if (Options.Project.FileScopedNamespace)
        {
            CodeBuilder.AppendLine(";");
            CodeBuilder.AppendLine();
            GenerateClass();
        }
        else
        {
            CodeBuilder.AppendLine();
            CodeBuilder.AppendLine("{");

            using (CodeBuilder.Indent())
            {
                GenerateClass();
            }

            CodeBuilder.AppendLine("}");
        }

        return CodeBuilder.ToString();
    }

    private void GenerateClass()
    {
        var entityClass = _entity.EntityClass.ToSafeName();

        if (Options.Data.Entity.Document)
        {
            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Entity class representing data for table '{_entity.TableName}'.");
            CodeBuilder.AppendLine("/// </summary>");
        }
        if (Options.Data.Entity.MappingAttributes)
        {
            CodeBuilder.AppendLine($"[System.ComponentModel.DataAnnotations.Schema.Table(\"{_entity.TableName}\", Schema = \"{_entity.TableSchema}\")]");
        }
        CodeBuilder.AppendLine($"public partial class {entityClass}");

        if (_entity.EntityBaseClass.HasValue())
        {
            var entityBaseClass = _entity.EntityBaseClass.ToSafeName();
            using (CodeBuilder.Indent())
                CodeBuilder.AppendLine($": {entityBaseClass}");
        }

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
            .OrderBy(r => r.PropertyName)
            .ToList();

        var entityClass = _entity.EntityClass.ToSafeName();

        if (Options.Data.Entity.Document)
        {
            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{entityClass}\"/> class.");
            CodeBuilder.AppendLine("/// </summary>");
        }

        CodeBuilder.AppendLine($"public {entityClass}()");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            CodeBuilder.AppendLine("#region Generated Constructor");
            foreach (var relationship in relationships)
            {
                var propertyName = relationship.PropertyName.ToSafeName();

                var primaryNamespace = relationship.PrimaryEntity.EntityNamespace;
                var primaryName = relationship.PrimaryEntity.EntityClass.ToSafeName();
                var primaryFullName = _entity.EntityNamespace != primaryNamespace
                    ? $"{primaryNamespace}.{primaryName}"
                    : primaryName;

                CodeBuilder.AppendLine($"{propertyName} = new HashSet<{primaryFullName}>();");
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
            var propertyType = property.SystemType.ToType();
            var propertyName = property.PropertyName.ToSafeName();

            if (Options.Data.Entity.Document)
            {
                CodeBuilder.AppendLine("/// <summary>");
                CodeBuilder.AppendLine($"/// Gets or sets the property value representing column '{property.ColumnName}'.");
                CodeBuilder.AppendLine("/// </summary>");
                CodeBuilder.AppendLine("/// <value>");
                CodeBuilder.AppendLine($"/// The property value representing column '{property.ColumnName}'.");
                CodeBuilder.AppendLine("/// </value>");
            }

            if (Options.Data.Entity.MappingAttributes)
            {
                if (property.IsPrimaryKey == true)
                {
                    CodeBuilder.AppendLine("[System.ComponentModel.DataAnnotations.Key()]");
                }

                if (property.IsConcurrencyToken == true)
                {
                    CodeBuilder.AppendLine("[System.ComponentModel.DataAnnotations.ConcurrencyCheck()]");
                }

                CodeBuilder.AppendLine($"[System.ComponentModel.DataAnnotations.Schema.Column(\"{property.ColumnName}\", TypeName = \"{property.StoreType}\")]");

                if (property.IsRowVersion == true || property.ValueGenerated == ValueGenerated.OnAddOrUpdate)
                {
                    CodeBuilder.AppendLine("[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed)]");
                }
                else if (property.ValueGenerated == ValueGenerated.OnAdd)
                {
                    CodeBuilder.AppendLine("[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]");
                }
            }

            if (property.IsNullable == true && (property.SystemType.IsValueType || Options.Project.Nullable))
                CodeBuilder.AppendLine($"public {propertyType}? {propertyName} {{ get; set; }}");
            else if (Options.Project.Nullable && !property.SystemType.IsValueType)
                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }} = null!;");
            else
                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }}");

            CodeBuilder.AppendLine();
        }
        CodeBuilder.AppendLine("#endregion");
        CodeBuilder.AppendLine();
    }

    private void GenerateRelationshipProperties()
    {
        CodeBuilder.AppendLine("#region Generated Relationships");
        foreach (var relationship in _entity.Relationships.OrderBy(r => r.PropertyName))
        {
            var propertyName = relationship.PropertyName.ToSafeName();
            var primaryNamespace = relationship.PrimaryEntity.EntityNamespace;
            var primaryName = relationship.PrimaryEntity.EntityClass.ToSafeName();
            var primaryFullName = _entity.EntityNamespace != primaryNamespace
                ? $"{primaryNamespace}.{primaryName}"
                : primaryName;

            if (relationship.Cardinality == Cardinality.Many)
            {
                if (Options.Data.Entity.Document)
                {
                    CodeBuilder.AppendLine("/// <summary>");
                    CodeBuilder.AppendLine($"/// Gets or sets the navigation collection for entity <see cref=\"{primaryFullName}\" />.");
                    CodeBuilder.AppendLine("/// </summary>");
                    CodeBuilder.AppendLine("/// <value>");
                    CodeBuilder.AppendLine($"/// The navigation collection for entity <see cref=\"{primaryFullName}\" />.");
                    CodeBuilder.AppendLine("/// </value>");
                }


                CodeBuilder.AppendLine($"public virtual ICollection<{primaryFullName}> {propertyName} {{ get; set; }}");
                CodeBuilder.AppendLine();
            }
            else
            {
                if (Options.Data.Entity.Document)
                {
                    CodeBuilder.AppendLine("/// <summary>");
                    CodeBuilder.AppendLine($"/// Gets or sets the navigation property for entity <see cref=\"{primaryFullName}\" />.");
                    CodeBuilder.AppendLine("/// </summary>");
                    CodeBuilder.AppendLine("/// <value>");
                    CodeBuilder.AppendLine($"/// The navigation property for entity <see cref=\"{primaryFullName}\" />.");
                    CodeBuilder.AppendLine("/// </value>");

                    foreach (var property in relationship.Properties)
                        CodeBuilder.AppendLine($"/// <seealso cref=\"{property.PropertyName}\" />");
                }

                if (!Options.Project.Nullable)
                    CodeBuilder.AppendLine($"public virtual {primaryFullName} {propertyName} {{ get; set; }}");
                else if (relationship.Cardinality == Cardinality.One)
                    CodeBuilder.AppendLine($"public virtual {primaryFullName} {propertyName} {{ get; set; }} = null!;");
                else
                    CodeBuilder.AppendLine($"public virtual {primaryFullName}? {propertyName} {{ get; set; }}");

                CodeBuilder.AppendLine();
            }
        }
        CodeBuilder.AppendLine("#endregion");
        CodeBuilder.AppendLine();
    }
}
