using System;

using FluentMigrator.Builders.Create.Table;

namespace EntityFrameworkCore.Generator.Migrator.Extensions;

public static class ColumnExtensions
{
    /// <summary>
    /// Applies identity generation to a column when the condition is true.
    /// </summary>
    public static ICreateTableColumnOptionOrWithColumnSyntax IdentityIf(
        this ICreateTableColumnOptionOrWithColumnSyntax column,
        bool condition)
    {
        ArgumentNullException.ThrowIfNull(column);

        return condition
            ? column.Identity()
            : column;
    }

    /// <summary>
    /// Applies identity generation to a column when the predicate returns true.
    /// </summary>
    public static ICreateTableColumnOptionOrWithColumnSyntax IdentityIf(
        this ICreateTableColumnOptionOrWithColumnSyntax column,
        Func<ICreateTableColumnOptionOrWithColumnSyntax, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(column);
        ArgumentNullException.ThrowIfNull(condition);

        return column.IdentityIf(condition(column));
    }
}
