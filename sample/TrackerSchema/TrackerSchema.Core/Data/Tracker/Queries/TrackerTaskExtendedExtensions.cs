using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Tracker.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended" />.
    /// </summary>
    public static partial class TrackerTaskExtendedExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="taskId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended GetByKey(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended> dbSet)
                return dbSet.Find(taskId);

            return queryable.FirstOrDefault(q => q.TaskId == taskId);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="taskId">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended"/> or null if not found.</returns>
        public static ValueTask<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended> GetByKeyAsync(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended> queryable, Guid taskId)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended> dbSet)
                return dbSet.FindAsync(taskId);

            var task = queryable.FirstOrDefaultAsync(q => q.TaskId == taskId);
            return new ValueTask<TrackerSchema.Core.Data.Tracker.Entities.TrackerTaskExtended>(task);
        }

        #endregion

    }
}
