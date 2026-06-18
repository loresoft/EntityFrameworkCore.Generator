using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

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
        if (Options.Data.Entity.MappingAttributes)
        {
            CodeBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
            CodeBuilder.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
        }
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
            CodeBuilder.AppendLine($"/// Entity class representing data for table <c>{_entity.TableName}</c>.");
            CodeBuilder.AppendLine("/// </summary>");
        }
        if (Options.Data.Entity.MappingAttributes)
        {
            if (_entity.TableSchema.HasValue())
                CodeBuilder.AppendLine($"[Table(\"{_entity.TableName}\", Schema = \"{_entity.TableSchema}\")]");
            else
                CodeBuilder.AppendLine($"[Table(\"{_entity.TableName}\")]");
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
            var propertyType = GetPropertyType(property);
            var propertyName = property.PropertyName.ToSafeName();

            if (Options.Data.Entity.Document)
            {
                CodeBuilder.AppendLine("/// <summary>");
                CodeBuilder.AppendLine($"/// Gets or sets the property value representing column <c>{property.ColumnName}</c>.");
                CodeBuilder.AppendLine("/// </summary>");
                CodeBuilder.AppendLine("/// <value>");
                CodeBuilder.AppendLine($"/// The property value representing column <c>{property.ColumnName}</c>.");
                CodeBuilder.AppendLine("/// </value>");
            }

            if (Options.Data.Entity.MappingAttributes)
            {
                if (property.IsPrimaryKey == true)
                {
                    CodeBuilder.AppendLine("[Key]");
                }

                if (property.IsConcurrencyToken == true)
                {
                    CodeBuilder.AppendLine("[ConcurrencyCheck]");
                }

                CodeBuilder.AppendLine($"[Column(\"{property.ColumnName}\", TypeName = \"{property.NativeType}\")]");

                if (property.IsIdentity == true)
                {
                    CodeBuilder.AppendLine("[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                }
                else if (property.IsRowVersion == true || property.IsComputed == true)
                {
                    CodeBuilder.AppendLine("[DatabaseGenerated(DatabaseGeneratedOption.Computed)]");
                }
            }

            if (property.IsNullable == true && (property.SystemType.IsValueType || Options.Project.Nullable))
                CodeBuilder.AppendLine($"public {ToNullablePropertyType(propertyType)} {propertyName} {{ get; set; }}");
            else if (Options.Project.Nullable && !property.SystemType.IsValueType)
                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }} = null!;");
            else
                CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }}");

            CodeBuilder.AppendLine();
        }
        CodeBuilder.AppendLine("#endregion");
        CodeBuilder.AppendLine();
    }

    private static string GetPropertyType(Property property)
    {
        return property.SystemTypeName.HasValue()
            ? property.SystemTypeName
            : property.SystemType.ToType();
    }

    private static string ToNullablePropertyType(string propertyType)
    {
        return propertyType.EndsWith('?')
            ? propertyType
            : propertyType + "?";
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
