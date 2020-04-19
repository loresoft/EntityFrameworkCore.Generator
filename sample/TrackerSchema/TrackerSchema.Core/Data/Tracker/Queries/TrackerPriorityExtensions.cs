using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Tracker.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority" />.
    /// </summary>
    public static partial class TrackerPriorityExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority GetByKey(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority"/> or null if not found.</returns>
        public static ValueTask<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority> GetByKeyAsync(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<TrackerSchema.Core.Data.Tracker.Entities.TrackerPriority>(task);
        }

        #endregion

    }
}
