using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Provides query extension methods for <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entities mapped to the <c>dbo.TaskExtended</c> table.
/// </summary>
public static partial class TaskExtendedExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entities.</param>
    /// <param name="taskId">The value to match against <see cref="Tracker.Core.Data.Entities.TaskExtended.TaskId" /> mapped to the <c>TaskId</c> column.</param>
    /// <returns>The matching <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static Tracker.Core.Data.Entities.TaskExtended? GetByKey(this IQueryable<Tracker.Core.Data.Entities.TaskExtended> queryable, Guid taskId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.TaskExtended> dbSet)
            return dbSet.Find(taskId);

        return queryable.FirstOrDefault(q => q.TaskId == taskId);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entities.</param>
    /// <param name="taskId">The value to match against <see cref="Tracker.Core.Data.Entities.TaskExtended.TaskId" /> mapped to the <c>TaskId</c> column.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching <see cref="Tracker.Core.Data.Entities.TaskExtended" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.TaskExtended?> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.TaskExtended> queryable, Guid taskId, CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.TaskExtended> dbSet)
            return await dbSet.FindAsync(new object[] { taskId }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.TaskId == taskId, cancellationToken);
    }

    #endregion

}
