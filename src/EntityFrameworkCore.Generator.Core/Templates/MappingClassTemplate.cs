using System.Globalization;
using System.Linq;

using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.Generator.Templates;

public class MappingClassTemplate : CodeTemplateBase
{
    private Entity _entity;

    public MappingClassTemplate(Entity entity, GeneratorOptions options) : base(options)
    {
        _entity = entity;
    }


    public override string WriteCode()
    {
        CodeBuilder.Clear();

        CodeBuilder.AppendLine("using System;");
        CodeBuilder.AppendLine("using System.Collections.Generic;");
        CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
        CodeBuilder.AppendLine();

        CodeBuilder.Append($"namespace {_entity.MappingNamespace}");

        if (Options.Data.Context.FileScopedNamespace)
        {
            CodeBuilder.AppendLine(";");
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
        var mappingClass = _entity.MappingClass.ToSafeName();
        var entityClass = _entity.EntityClass.ToSafeName();
        var safeName = $"{_entity.EntityNamespace}.{entityClass}";

        if (Options.Data.Mapping.Document)
        {
            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Allows configuration for an entity type <see cref=\"{safeName}\" />");
            CodeBuilder.AppendLine("/// </summary>");
        }

        CodeBuilder.AppendLine($"public partial class {mappingClass}");

        using (CodeBuilder.Indent())
            CodeBuilder.AppendLine($": IEntityTypeConfiguration<{safeName}>");

        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            GenerateConfigure();
            GenerateConstants();
        }

        CodeBuilder.AppendLine("}");

    }

    private void GenerateConstants()
    {
        var entityClass = _entity.EntityClass.ToSafeName();
        var safeName = $"{_entity.EntityNamespace}.{entityClass}";

        CodeBuilder.AppendLine("#region Generated Constants");

        CodeBuilder.AppendLine("public struct Table");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {

            if (Options.Data.Mapping.Document)
                CodeBuilder.AppendLine($"/// <summary>Table Schema name constant for entity <see cref=\"{safeName}\" /></summary>");

            CodeBuilder.AppendLine($"public const string Schema = \"{_entity.TableSchema}\";");

            if (Options.Data.Mapping.Document)
                CodeBuilder.AppendLine($"/// <summary>Table Name constant for entity <see cref=\"{safeName}\" /></summary>");

            CodeBuilder.AppendLine($"public const string Name = \"{_entity.TableName}\";");
        }

        CodeBuilder.AppendLine("}");

        CodeBuilder.AppendLine();
        CodeBuilder.AppendLine("public struct Columns");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            foreach (var property in _entity.Properties)
            {
                if (Options.Data.Mapping.Document)
                    CodeBuilder.AppendLine($"/// <summary>Column Name constant for property <see cref=\"{safeName}.{property.PropertyName}\" /></summary>");

                CodeBuilder.AppendLine($"public const string {property.PropertyName.ToSafeName()} = {property.ColumnName.ToLiteral()};");
            }
        }

        CodeBuilder.AppendLine("}");
        CodeBuilder.AppendLine("#endregion");
    }

    private void GenerateConfigure()
    {
        var entityClass = _entity.EntityClass.ToSafeName();
        var entityFullName = $"{_entity.EntityNamespace}.{entityClass}";

        if (Options.Data.Mapping.Document)
        {
            CodeBuilder.AppendLine("/// <summary>");
            CodeBuilder.AppendLine($"/// Configures the entity of type <see cref=\"{entityFullName}\" />");
            CodeBuilder.AppendLine("/// </summary>");
            CodeBuilder.AppendLine("/// <param name=\"builder\">The builder to be used to configure the entity type.</param>");
        }

        CodeBuilder.AppendLine($"public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<{entityFullName}> builder)");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            CodeBuilder.AppendLine("#region Generated Configure");

            GenerateTableMapping();
            GenerateKeyMapping();
            GeneratePropertyMapping();
            GenerateRelationshipMapping();

            CodeBuilder.AppendLine("#endregion");
        }

        CodeBuilder.AppendLine("}");
        CodeBuilder.AppendLine();
    }


    private void GenerateRelationshipMapping()
    {
        CodeBuilder.AppendLine("// relationships");
        foreach (var relationship in _entity.Relationships.Where(e => e.IsMapped))
        {
            GenerateRelationshipMapping(relationship);
            CodeBuilder.AppendLine();
        }

    }

    private void GenerateRelationshipMapping(Relationship relationship)
    {
        CodeBuilder.Append("builder.HasOne(t => t.");
        CodeBuilder.Append(relationship.PropertyName);
        CodeBuilder.Append(")");
        CodeBuilder.AppendLine();

        CodeBuilder.IncrementIndent();

        CodeBuilder.Append(relationship.PrimaryCardinality == Cardinality.Many
            ? ".WithMany(t => t."
            : ".WithOne(t => t.");

        CodeBuilder.Append(relationship.PrimaryPropertyName);
        CodeBuilder.Append(")");

        CodeBuilder.AppendLine();
        CodeBuilder.Append(".HasForeignKey");
        if (relationship.IsOneToOne)
        {
            CodeBuilder.Append("<");
            CodeBuilder.Append(_entity.EntityNamespace);
            CodeBuilder.Append(".");
            CodeBuilder.Append(_entity.EntityClass.ToSafeName());
            CodeBuilder.Append(">");
        }
        CodeBuilder.Append("(d => ");

        var keys = relationship.Properties;
        bool wroteLine = false;

        if (keys.Count == 1)
        {
            var propertyName = keys.First().PropertyName.ToSafeName();
            CodeBuilder.Append($"d.{propertyName}");
        }
        else
        {
            CodeBuilder.Append("new { ");
            foreach (var p in keys)
            {
                if (wroteLine)
                    CodeBuilder.Append(", ");

                CodeBuilder.Append($"d.{p.PropertyName}");
                wroteLine = true;
            }
            CodeBuilder.Append("}");
        }
        CodeBuilder.Append(")");

        if (!string.IsNullOrEmpty(relationship.RelationshipName))
        {
            CodeBuilder.AppendLine();
            CodeBuilder.Append(".HasConstraintName(\"");
            CodeBuilder.Append(relationship.RelationshipName);
            CodeBuilder.Append("\")");
        }

        var cascadeOption = $"{nameof(DeleteBehavior)}.{nameof(DeleteBehavior.NoAction)}";
        if (relationship.CascadeDelete == true)
        {
            cascadeOption = $"{nameof(DeleteBehavior)}.{Options.Data.Mapping.RelationshipDeleteBehavior}";
        }
        CodeBuilder.AppendLine();
        CodeBuilder.Append($".OnDelete({cascadeOption})");

        CodeBuilder.DecrementIndent();

        CodeBuilder.AppendLine(";");
    }


    private void GeneratePropertyMapping()
    {
        CodeBuilder.AppendLine("// properties");
        foreach (var property in _entity.Properties)
        {
            GeneratePropertyMapping(property);
            CodeBuilder.AppendLine();
        }
    }

    private void GeneratePropertyMapping(Property property)
    {
        bool isString = property.SystemType == typeof(string);
        bool isByteArray = property.SystemType == typeof(byte[]);

        CodeBuilder.Append($"builder.Property(t => t.{property.PropertyName})");

        CodeBuilder.IncrementIndent();
        if (property.IsRequired)
        {
            CodeBuilder.AppendLine();
            CodeBuilder.Append(".IsRequired()");
        }

        if (property.IsRowVersion == true)
        {
            CodeBuilder.AppendLine();
            CodeBuilder.Append(".IsRowVersion()");
        }

        CodeBuilder.AppendLine();
        CodeBuilder.Append($".HasColumnName({property.ColumnName.ToLiteral()})");

        if (!string.IsNullOrEmpty(property.StoreType))
        {
            CodeBuilder.AppendLine();
            CodeBuilder.Append($".HasColumnType({property.StoreType.ToLiteral()})");
        }

        if ((isString || isByteArray) && property.Size > 0)
        {
            CodeBuilder.AppendLine();
            CodeBuilder.Append($".HasMaxLength({property.Size.Value.ToString(CultureInfo.InvariantCulture)})");
        }

        if (!string.IsNullOrEmpty(property.Default))
        {
            CodeBuilder.AppendLine();
            CodeBuilder.Append($".HasDefaultValueSql({property.Default.ToLiteral()})");
        }

        switch (property.ValueGenerated)
        {
            case ValueGenerated.OnAdd:
                CodeBuilder.AppendLine();
                CodeBuilder.Append(".ValueGeneratedOnAdd()");
                break;
            case ValueGenerated.OnAddOrUpdate:
                CodeBuilder.AppendLine();
                CodeBuilder.Append(".ValueGeneratedOnAddOrUpdate()");
                break;
            case ValueGenerated.OnUpdate:
                CodeBuilder.AppendLine();
                CodeBuilder.Append(".ValueGeneratedOnUpdate()");
                break;
        }
        CodeBuilder.DecrementIndent();

        CodeBuilder.AppendLine(";");
    }


    private void GenerateKeyMapping()
    {
        CodeBuilder.AppendLine("// key");

        var keys = _entity.Properties.Where(p => p.IsPrimaryKey == true).ToList();
        if (keys.Count == 0)
        {
            CodeBuilder.AppendLine("builder.HasNoKey();");
            CodeBuilder.AppendLine();

            return;
        }

        CodeBuilder.Append("builder.HasKey(t => ");

        if (keys.Count == 1)
        {
            var propertyName = keys.First().PropertyName.ToSafeName();
            CodeBuilder.AppendLine($"t.{propertyName});");
            CodeBuilder.AppendLine();

            return;
        }

        bool wroteLine = false;

        CodeBuilder.Append("new { ");
        foreach (var p in keys)
        {
            if (wroteLine)
                CodeBuilder.Append(", ");

            CodeBuilder.Append("t.");
            CodeBuilder.Append(p.PropertyName);
            wroteLine = true;
        }

        CodeBuilder.AppendLine(" });");
        CodeBuilder.AppendLine();
    }

    private void GenerateTableMapping()
    {
        CodeBuilder.AppendLine("// table");

        var method = _entity.IsView
            ? nameof(RelationalEntityTypeBuilderExtensions.ToView)
            : nameof(RelationalEntityTypeBuilderExtensions.ToTable);

        CodeBuilder.AppendLine(_entity.TableSchema.HasValue()
            ? $"builder.{method}(\"{_entity.TableName}\", \"{_entity.TableSchema}\");"
            : $"builder.{method}(\"{_entity.TableName}\");");

        CodeBuilder.AppendLine();
    }
}
