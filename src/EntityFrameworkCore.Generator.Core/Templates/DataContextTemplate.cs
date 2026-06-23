using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates;

public class DataContextTemplate : CodeTemplateBase
{
    private readonly EntityContext _entityContext;

    public DataContextTemplate(EntityContext entityContext, GeneratorOptions options) : base(options)
    {
        _entityContext = entityContext;
    }

    public override string WriteCode()
    {
        CodeBuilder.Clear();

        if (Options.Data.Context.Header.HasValue())
            CodeBuilder.AppendLine(Options.Data.Context.Header).AppendLine();

        CodeBuilder.AppendLine("using System;");
        CodeBuilder.AppendLine();
        CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
        CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore.Metadata;");
        CodeBuilder.AppendLine();

        CodeBuilder.Append($"namespace {_entityContext.ContextNamespace}");

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
        var contextClass = _entityContext.ContextClass.ToSafeName();
        var baseClass = _entityContext.ContextBaseClass.ToSafeName();

        if (Options.Data.Context.Document)
        {
            GenerateClassDocumentation();
        }

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

        if (Options.Data.Context.Document)
        {
            GenerateConstructorDocumentation(contextName);
        }

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
        foreach (var entityType in _entityContext.Entities.OrderBy(e => e.ContextProperty))
        {
            var entityClass = entityType.EntityClass.ToSafeName();
            var propertyName = entityType.ContextProperty.ToSafeName();
            var fullName = $"{entityType.EntityNamespace}.{entityClass}";

            if (Options.Data.Context.Document)
            {
                GenerateDbSetDocumentation(entityType, fullName);
            }

            CodeBuilder.Append($"public virtual DbSet<{fullName}> {propertyName} {{ get; set; }}");
            if (Options.Project.Nullable)
                CodeBuilder.Append(" = null!;");

            CodeBuilder.AppendLine();
            CodeBuilder.AppendLine();
        }

        CodeBuilder.AppendLine("#endregion");

        if (_entityContext.Entities.Any())
            CodeBuilder.AppendLine();
    }

    private void GenerateOnConfiguring()
    {
        if (Options.Data.Context.Document)
        {
            GenerateOnModelCreatingDocumentation();
        }

        CodeBuilder.AppendLine("protected override void OnModelCreating(ModelBuilder modelBuilder)");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            CodeBuilder.AppendLine("#region Generated Configuration");
            foreach (var entityType in _entityContext.Entities.OrderBy(e => e.MappingClass))
            {
                var mappingClass = entityType.MappingClass.ToSafeName();

                CodeBuilder.AppendLine($"modelBuilder.ApplyConfiguration(new {entityType.MappingNamespace}.{mappingClass}());");
            }

            CodeBuilder.AppendLine("#endregion");
        }

        CodeBuilder.AppendLine("}");
    }

    private void GenerateClassDocumentation()
    {
        var databaseName = ToXmlText(_entityContext.DatabaseName);

        CodeBuilder.AppendLine("/// <summary>");

        if (databaseName.HasValue())
            CodeBuilder.AppendLine($"/// Represents a session with the <c>{databaseName}</c> database and provides access to generated entity sets.");
        else
            CodeBuilder.AppendLine("/// Represents a session with the database and provides access to generated entity sets.");

        CodeBuilder.AppendLine("/// </summary>");
    }

    private void GenerateConstructorDocumentation(string contextName)
    {
        CodeBuilder.AppendLine("/// <summary>");
        CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{contextName}\"/> class.");
        CodeBuilder.AppendLine("/// </summary>");
        CodeBuilder.AppendLine("/// <param name=\"options\">The options used to configure this <see cref=\"DbContext\" /> instance.</param>");
    }

    private void GenerateDbSetDocumentation(Entity entityType, string fullName)
    {
        var propertyName = ToXmlText(entityType.ContextProperty);
        var sourceName = ToXmlText(GetQualifiedTableName(entityType));
        var sourceType = entityType.IsView ? "view" : "table";

        CodeBuilder.AppendLine("/// <summary>");

        if (sourceName.HasValue())
            CodeBuilder.AppendLine($"/// Gets or sets the <see cref=\"DbSet{{TEntity}}\" /> for <see cref=\"{fullName}\" /> entities mapped to the <c>{sourceName}</c> {sourceType}.");
        else
            CodeBuilder.AppendLine($"/// Gets or sets the <see cref=\"DbSet{{TEntity}}\" /> for <see cref=\"{fullName}\" /> entities.");

        CodeBuilder.AppendLine("/// </summary>");
        CodeBuilder.AppendLine("/// <value>");
        CodeBuilder.AppendLine($"/// The <c>{propertyName}</c> entity set.");
        CodeBuilder.AppendLine("/// </value>");
    }

    private void GenerateOnModelCreatingDocumentation()
    {
        CodeBuilder.AppendLine("/// <summary>");
        CodeBuilder.AppendLine("/// Configures entity mappings for the generated model.");
        CodeBuilder.AppendLine("/// </summary>");
        CodeBuilder.AppendLine("/// <param name=\"modelBuilder\">The builder used to configure the generated entity model.</param>");
    }

    private static string? GetQualifiedTableName(Entity entityType)
    {
        if (entityType.TableName.IsNullOrEmpty())
            return entityType.TableName;

        return entityType.TableSchema.HasValue()
            ? $"{entityType.TableSchema}.{entityType.TableName}"
            : entityType.TableName;
    }
}
