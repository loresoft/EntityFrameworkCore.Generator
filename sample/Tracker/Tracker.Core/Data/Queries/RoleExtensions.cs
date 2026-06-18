using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Query extensions for entity <see cref="Tracker.Core.Data.Entities.Role" />.
/// </summary>
public static partial class RoleExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Gets an instance by the primary key.
    /// </summary>
    /// <param name="queryable">An <see cref="IQueryable`1" /> to filter.</param>
    /// <param name="id">The value to filter by.</param>
    /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> or null if not found.</returns>
    public static Tracker.Core.Data.Entities.Role? GetByKey(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, Guid id)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.Role> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    /// <summary>
    /// Gets an instance by the primary key.
    /// </summary>
    /// <param name="queryable">An <see cref="IQueryable`1" /> to filter.</param>
    /// <param name="id">The value to filter by.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> or null if not found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.Role?> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, Guid id, CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.Role> dbSet)
            return await dbSet.FindAsync(new object[] { id }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    /// <summary>
    /// Gets an instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> by using a unique index.
    /// </summary>
    /// <param name="queryable">An <see cref="IQueryable`1" /> to filter.</param>
    /// <param name="name">The value to filter by.</param>
    /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> or null if not found.</returns>
    public static Tracker.Core.Data.Entities.Role? GetByName(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, string name)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.FirstOrDefault(q => q.Name == name);
    }

    /// <summary>
    /// Gets an instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> by using a unique index.
    /// </summary>
    /// <param name="queryable">An <see cref="IQueryable`1" /> to filter.</param>
    /// <param name="name">The value to filter by.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> or null if not found.</returns>
    public static async System.Threading.Tasks.Task<Tracker.Core.Data.Entities.Role?> GetByNameAsync(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, string name, CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return await queryable.FirstOrDefaultAsync(q => q.Name == name, cancellationToken);
    }

    #endregion

}
