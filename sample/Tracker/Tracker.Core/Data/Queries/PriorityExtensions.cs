using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Provides query extension methods for <see cref="Tracker.Core.Data.Entities.Priority" /> entities mapped to the <c>dbo.Priority</c> table.
/// </summary>
public static partial class PriorityExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.Priority" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Priority" /> entities.</param>
    /// <param name="id">The value to match against <see cref="Tracker.Core.Data.Entities.Priority.Id" /> mapped to the <c>Id</c> column.</param>
    /// <returns>The matching <see cref="Tracker.Core.Data.Entities.Priority" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static Tracker.Core.Data.Entities.Priority? GetByKey(this IQueryable<Tracker.Core.Data.Entities.Priority> queryable, Guid id)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.Priority> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.Priority" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.Priority" /> entities.</param>
    /// <param name="id">The value to match against <see cref="Tracker.Core.Data.Entities.Priority.Id" /> mapped to the <c>Id</c> column.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching <see cref="Tracker.Core.Data.Entities.Priority" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.Priority?> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.Priority> queryable, Guid id, CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.Priority> dbSet)
            return await dbSet.FindAsync(new object[] { id }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    #endregion

}
