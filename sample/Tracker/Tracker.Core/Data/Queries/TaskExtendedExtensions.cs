using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Query extensions for entity <see cref="Tracker.Core.Data.Entities.TaskExtended" />.
/// </summary>
public static partial class TaskExtendedExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Gets an instance by the primary key.
    /// </summary>
    /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
    /// <param name="taskId">The value to filter by.</param>
    /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.TaskExtended"/> or null if not found.</returns>
    public static Tracker.Core.Data.Entities.TaskExtended? GetByKey(this System.Linq.IQueryable<Tracker.Core.Data.Entities.TaskExtended> queryable, Guid taskId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.TaskExtended> dbSet)
            return dbSet.Find(taskId);

        return queryable.FirstOrDefault(q => q.TaskId == taskId);
    }

    /// <summary>
    /// Gets an instance by the primary key.
    /// </summary>
    /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
    /// <param name="taskId">The value to filter by.</param>
    /// <param name="cancellationToken">A <see cref="System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.TaskExtended"/> or null if not found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.TaskExtended?> GetByKeyAsync(this System.Linq.IQueryable<Tracker.Core.Data.Entities.TaskExtended> queryable, Guid taskId, System.Threading.CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.TaskExtended> dbSet)
            return await dbSet.FindAsync(new object[] { taskId }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.TaskId == taskId, cancellationToken);
    }

    #endregion

}
