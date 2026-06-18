using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Provides query extension methods for <see cref="Tracker.Core.Data.Entities.Role" /> entities mapped to the <c>dbo.Role</c> table.
/// </summary>
public static partial class RoleExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.Role" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Role" /> entities.</param>
    /// <param name="id">The value to match against <see cref="Tracker.Core.Data.Entities.Role.Id" /> mapped to the <c>Id</c> column.</param>
    /// <returns>The matching <see cref="Tracker.Core.Data.Entities.Role" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static Tracker.Core.Data.Entities.Role? GetByKey(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, Guid id)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.Role> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.Role" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Role" /> entities.</param>
    /// <param name="id">The value to match against <see cref="Tracker.Core.Data.Entities.Role.Id" /> mapped to the <c>Id</c> column.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching <see cref="Tracker.Core.Data.Entities.Role" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.Role?> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, Guid id, CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.Role> dbSet)
            return await dbSet.FindAsync(new object[] { id }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.Role" /> entity matching the unique index <c>UX_Role_Name</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Role" /> entities.</param>
    /// <param name="name">The value to match against <see cref="Tracker.Core.Data.Entities.Role.Name" /> mapped to the <c>Name</c> column.</param>
    /// <returns>The matching <see cref="Tracker.Core.Data.Entities.Role" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static Tracker.Core.Data.Entities.Role? GetByName(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, string name)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.FirstOrDefault(q => q.Name == name);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.Role" /> entity matching the unique index <c>UX_Role_Name</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Role" /> entities.</param>
    /// <param name="name">The value to match against <see cref="Tracker.Core.Data.Entities.Role.Name" /> mapped to the <c>Name</c> column.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching <see cref="Tracker.Core.Data.Entities.Role" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static async System.Threading.Tasks.Task<Tracker.Core.Data.Entities.Role?> GetByNameAsync(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, string name, CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return await queryable.FirstOrDefaultAsync(q => q.Name == name, cancellationToken);
    }

    #endregion

}
