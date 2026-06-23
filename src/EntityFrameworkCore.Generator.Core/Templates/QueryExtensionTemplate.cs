using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates;

public class QueryExtensionTemplate : CodeTemplateBase
{
    private readonly Entity _entity;

    public QueryExtensionTemplate(Entity entity, GeneratorOptions options) : base(options)
    {
        _entity = entity;
    }

    public override string WriteCode()
    {
        CodeBuilder.Clear();

        if (Options.Data.Query.Header.HasValue())
            CodeBuilder.AppendLine(Options.Data.Query.Header).AppendLine();

        CodeBuilder.AppendLine("using System;");
        CodeBuilder.AppendLine("using System.Collections.Generic;");
        CodeBuilder.AppendLine("using System.Linq;");
        CodeBuilder.AppendLine("using System.Threading;");
        CodeBuilder.AppendLine("using System.Threading.Tasks;");
        CodeBuilder.AppendLine();
        CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
        CodeBuilder.AppendLine();

        var extensionNamespace = Options.Data.Query.Namespace;

        CodeBuilder.Append($"namespace {extensionNamespace}");

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
        string safeName = _entity.EntityNamespace + "." + entityClass;

        if (Options.Data.Query.Document)
        {
            GenerateClassDocumentation(safeName);
        }

        CodeBuilder.AppendLine($"public static partial class {entityClass}Extensions");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            GenerateMethods();
        }

        CodeBuilder.AppendLine("}");

    }

    private void GenerateMethods()
    {
        CodeBuilder.AppendLine("#region Generated Extensions");
        foreach (var method in _entity.Methods.OrderBy(m => m.NameSuffix))
        {
            if (method.IsKey)
            {
                GenerateKeyMethod(method);
                GenerateKeyMethod(method, true);
            }
            else if (method.IsUnique)
            {
                GenerateUniqueMethod(method);
                GenerateUniqueMethod(method, true);
            }
            else
            {
                GenerateMethod(method);
            }
        }
        CodeBuilder.AppendLine("#endregion");
        CodeBuilder.AppendLine();

    }

    private void GenerateMethod(Method method)
    {
        var safeName = _entity.EntityNamespace + "." + _entity.EntityClass.ToSafeName();
        var prefix = Options.Data.Query.IndexPrefix;
        var suffix = method.NameSuffix;

        if (Options.Data.Query.Document)
        {
            GenerateFilterMethodDocumentation(method, safeName);
        }

        CodeBuilder.Append($"public static IQueryable<{safeName}> {prefix}{suffix}(this IQueryable<{safeName}> queryable, ");
        AppendParameters(method);
        CodeBuilder.AppendLine(")");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            CodeBuilder.AppendLine("if (queryable is null)");
            using (CodeBuilder.Indent())
                CodeBuilder.AppendLine("throw new ArgumentNullException(nameof(queryable));");
            CodeBuilder.AppendLine();

            CodeBuilder.Append("return queryable.Where(");
            AppendLamba(method);
            CodeBuilder.AppendLine(");");
        }

        CodeBuilder.AppendLine("}");
        CodeBuilder.AppendLine();
    }

    private void GenerateUniqueMethod(Method method, bool async = false)
    {
        var safeName = _entity.EntityNamespace + "." + _entity.EntityClass.ToSafeName();
        var uniquePrefix = Options.Data.Query.UniquePrefix;
        var suffix = method.NameSuffix;

        var asyncSuffix = async ? "Async" : string.Empty;
        var asyncPrefix = async ? "async " : string.Empty;
        var awaitPrefix = async ? "await " : string.Empty;
        var nullableSuffix = Options.Project.Nullable ? "?" : "";
        var returnType = async ? $"System.Threading.Tasks.Task<{safeName}{nullableSuffix}>" : safeName + nullableSuffix;

        if (Options.Data.Query.Document)
        {
            GenerateUniqueMethodDocumentation(method, safeName);

            if (async)
                GenerateCancellationTokenDocumentation();

            GenerateSingleEntityReturnDocumentation(safeName, async);
        }

        CodeBuilder.Append($"public static {asyncPrefix}{returnType} {uniquePrefix}{suffix}{asyncSuffix}(this IQueryable<{safeName}> queryable, ");
        AppendParameters(method);

        if (async)
            CodeBuilder.Append(", CancellationToken cancellationToken = default");

        CodeBuilder.AppendLine(")");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            CodeBuilder.AppendLine("if (queryable is null)");
            using (CodeBuilder.Indent())
                CodeBuilder.AppendLine("throw new ArgumentNullException(nameof(queryable));");
            CodeBuilder.AppendLine();


            CodeBuilder.Append($"return {awaitPrefix}queryable.FirstOrDefault{asyncSuffix}(");

            AppendLamba(method);

            if (async)
                CodeBuilder.Append(", cancellationToken");

            CodeBuilder.AppendLine(");");
        }

        CodeBuilder.AppendLine("}");
        CodeBuilder.AppendLine();
    }

    private void GenerateKeyMethod(Method method, bool async = false)
    {
        var safeName = _entity.EntityNamespace + "." + _entity.EntityClass.ToSafeName();
        var uniquePrefix = Options.Data.Query.UniquePrefix;

        var asyncSuffix = async ? "Async" : string.Empty;
        var asyncPrefix = async ? "async " : string.Empty;
        var awaitPrefix = async ? "await " : string.Empty;
        var nullableSuffix = Options.Project.Nullable ? "?" : "";
        var returnType = async ? $"System.Threading.Tasks.ValueTask<{safeName}{nullableSuffix}>" : safeName + nullableSuffix;

        if (Options.Data.Query.Document)
        {
            GenerateKeyMethodDocumentation(method, safeName);

            if (async)
                GenerateCancellationTokenDocumentation();

            GenerateSingleEntityReturnDocumentation(safeName, async);
        }

        CodeBuilder.Append($"public static {asyncPrefix}{returnType} {uniquePrefix}Key{asyncSuffix}(this IQueryable<{safeName}> queryable, ");
        AppendParameters(method);

        if (async)
            CodeBuilder.Append(", CancellationToken cancellationToken = default");

        CodeBuilder.AppendLine(")");
        CodeBuilder.AppendLine("{");

        using (CodeBuilder.Indent())
        {
            CodeBuilder.AppendLine("if (queryable is null)");
            using (CodeBuilder.Indent())
                CodeBuilder.AppendLine("throw new ArgumentNullException(nameof(queryable));");
            CodeBuilder.AppendLine();

            CodeBuilder.AppendLine($"if (queryable is DbSet<{safeName}> dbSet)");
            using (CodeBuilder.Indent())
            {
                CodeBuilder.Append($"return {awaitPrefix}dbSet.Find{asyncSuffix}(");

                if (async)
                    CodeBuilder.Append("new object[] { ");

                AppendNames(method);

                if (async)
                    CodeBuilder.Append(" }, cancellationToken");

                CodeBuilder.AppendLine(");");
            }

            CodeBuilder.AppendLine("");
            CodeBuilder.Append($"return {awaitPrefix}queryable.FirstOrDefault{asyncSuffix}(");

            AppendLamba(method);

            if (async)
                CodeBuilder.Append(", cancellationToken");

            CodeBuilder.AppendLine(");");
        }
        CodeBuilder.AppendLine("}");
        CodeBuilder.AppendLine();
    }


    private void GenerateClassDocumentation(string entityFullName)
    {
        var sourceName = ToXmlText(GetQualifiedTableName());
        var sourceType = _entity.IsView ? "view" : "table";

        CodeBuilder.AppendLine("/// <summary>");

        if (sourceName.HasValue())
            CodeBuilder.AppendLine($"/// Provides query extension methods for <see cref=\"{entityFullName}\" /> entities mapped to the <c>{sourceName}</c> {sourceType}.");
        else
            CodeBuilder.AppendLine($"/// Provides query extension methods for <see cref=\"{entityFullName}\" /> entities.");

        CodeBuilder.AppendLine("/// </summary>");
    }

    private void GenerateFilterMethodDocumentation(Method method, string entityFullName)
    {
        CodeBuilder.AppendLine("/// <summary>");
        CodeBuilder.AppendLine($"/// Filters <see cref=\"{entityFullName}\" /> entities by {QueryExtensionTemplate.FormatPropertyList(method)}.");
        CodeBuilder.AppendLine("/// </summary>");

        GenerateQueryableParameterDocumentation(entityFullName);
        GenerateParameterDocumentation(method, entityFullName);

        CodeBuilder.AppendLine($"/// <returns>An <see cref=\"IQueryable{{T}}\" /> of <see cref=\"{entityFullName}\" /> entities matching the specified values.</returns>");
    }

    private void GenerateUniqueMethodDocumentation(Method method, string entityFullName)
    {
        var sourceName = ToXmlText(method.SourceName);

        CodeBuilder.AppendLine("/// <summary>");

        if (sourceName.HasValue())
            CodeBuilder.AppendLine($"/// Gets the <see cref=\"{entityFullName}\" /> entity matching the unique index <c>{sourceName}</c>.");
        else
            CodeBuilder.AppendLine($"/// Gets the <see cref=\"{entityFullName}\" /> entity matching the unique {QueryExtensionTemplate.FormatPropertyList(method)} values.");

        CodeBuilder.AppendLine("/// </summary>");

        GenerateQueryableParameterDocumentation(entityFullName);
        GenerateParameterDocumentation(method, entityFullName);
    }

    private void GenerateKeyMethodDocumentation(Method method, string entityFullName)
    {
        CodeBuilder.AppendLine("/// <summary>");
        CodeBuilder.AppendLine($"/// Gets the <see cref=\"{entityFullName}\" /> entity matching the primary key.");
        CodeBuilder.AppendLine("/// </summary>");

        GenerateQueryableParameterDocumentation(entityFullName);
        GenerateParameterDocumentation(method, entityFullName);
    }

    private void GenerateQueryableParameterDocumentation(string entityFullName)
    {
        CodeBuilder.AppendLine($"/// <param name=\"queryable\">The source query for <see cref=\"{entityFullName}\" /> entities.</param>");
    }

    private void GenerateCancellationTokenDocumentation()
    {
        CodeBuilder.AppendLine("/// <param name=\"cancellationToken\">A <see cref=\"CancellationToken\" /> to observe while waiting for the operation to complete.</param>");
    }

    private void GenerateSingleEntityReturnDocumentation(string entityFullName, bool async)
    {
        var asyncPrefix = async ? "A task that represents the asynchronous operation. The task result contains" : "";
        var article = async ? " the" : "The";

        CodeBuilder.AppendLine($"/// <returns>{asyncPrefix}{article} matching <see cref=\"{entityFullName}\" /> entity, or <see langword=\"null\" /> if no match is found.</returns>");
    }

    private void GenerateParameterDocumentation(Method method, string entityFullName)
    {
        foreach (var property in method.Properties)
        {
            string paramName = property.PropertyName
                .ToCamelCase()
                .ToSafeName();

            var propertyName = property.PropertyName.ToSafeName();
            var columnName = ToXmlText(property.ColumnName);

            if (columnName.HasValue())
                CodeBuilder.AppendLine($"/// <param name=\"{paramName}\">The value to match against <see cref=\"{entityFullName}.{propertyName}\" /> mapped to the <c>{columnName}</c> column.</param>");
            else
                CodeBuilder.AppendLine($"/// <param name=\"{paramName}\">The value to match against <see cref=\"{entityFullName}.{propertyName}\" />.</param>");
        }
    }

    private static string FormatPropertyList(Method method)
    {
        var values = method.Properties
            .Select(property => ToXmlText(property.PropertyName.ToSafeName()) ?? string.Empty)
            .ToList();

        return values.Count switch
        {
            0 => "the generated criteria",
            1 => $"<c>{values[0]}</c>",
            2 => $"<c>{values[0]}</c> and <c>{values[1]}</c>",
            _ => $"{string.Join(", ", values.Take(values.Count - 1).Select(value => $"<c>{value}</c>"))}, and <c>{values[^1]}</c>"
        };
    }

    private string? GetQualifiedTableName()
    {
        if (_entity.TableName.IsNullOrEmpty())
            return _entity.TableName;

        return _entity.TableSchema.HasValue()
            ? $"{_entity.TableSchema}.{_entity.TableName}"
            : _entity.TableName;
    }

    private void AppendParameters(Method method)
    {
        bool wrote = false;

        foreach (var property in method.Properties)
        {
            if (wrote)
                CodeBuilder.Append(", ");

            var paramName = property.PropertyName
                .ToCamelCase()
                .ToSafeName();

            var paramType = property.SystemType
                .ToNullableType(property.IsNullable == true);

            CodeBuilder.Append($"{paramType} {paramName}");

            wrote = true;
        }
    }

    private void AppendNames(Method method)
    {
        bool wrote = false;
        foreach (var property in method.Properties)
        {
            if (wrote)
                CodeBuilder.Append(", ");

            string paramName = property.PropertyName
                .ToCamelCase()
                .ToSafeName();

            CodeBuilder.Append(paramName);
            wrote = true;
        }
    }

    private void AppendLamba(Method method)
    {
        bool wrote = false;
        bool indented = false;

        foreach (var property in method.Properties)
        {
            string paramName = property.PropertyName
                .ToCamelCase()
                .ToSafeName();

            if (!wrote)
            {
                CodeBuilder.Append("q => ");
            }
            else
            {
                CodeBuilder.AppendLine();
                CodeBuilder.IncrementIndent();
                CodeBuilder.Append("&& ");

                indented = true;
            }

            if (property.IsNullable == true)
                CodeBuilder.Append($"(q.{property.PropertyName} == {paramName} || ({paramName} == null && q.{property.PropertyName} == null))");
            else
                CodeBuilder.Append($"q.{property.PropertyName} == {paramName}");

            wrote = true;
        }

        if (indented)
            CodeBuilder.DecrementIndent();
    }
}
