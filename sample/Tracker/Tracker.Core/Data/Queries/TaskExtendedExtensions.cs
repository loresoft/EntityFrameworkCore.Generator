using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries
{
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
        public static Tracker.Core.Data.Entities.TaskExtended GetByKey(this IQueryable<Tracker.Core.Data.Entities.TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.TaskExtended> dbSet)
                return dbSet.Find(taskId);

            return queryable.FirstOrDefault(q => q.TaskId == taskId);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="taskId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.TaskExtended"/> or null if not found.</returns>
        public static ValueTask<Tracker.Core.Data.Entities.TaskExtended> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.TaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.TaskExtended> dbSet)
                return dbSet.FindAsync(taskId);

            var task = queryable.FirstOrDefaultAsync(q => q.TaskId == taskId);
            return new ValueTask<Tracker.Core.Data.Entities.TaskExtended>(task);
        }

        #endregion

    }
}
