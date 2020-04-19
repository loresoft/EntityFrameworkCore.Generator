using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Tracker.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="TrackerSchema.Core.Data.Tracker.Entities.TrackerTask" />.
    /// </summary>
    public static partial class TrackerTaskExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="assignedId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> ByAssignedId(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> queryable, Guid? assignedId)
        {
            return queryable.Where(q => (q.AssignedId == assignedId || (assignedId == null && q.AssignedId == null)));
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Tracker.Entities.TrackerTask"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Tracker.Entities.TrackerTask GetByKey(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Tracker.Entities.TrackerTask"/> or null if not found.</returns>
        public static ValueTask<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> GetByKeyAsync(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask>(task);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="priorityId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> ByPriorityId(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> queryable, Guid? priorityId)
        {
            return queryable.Where(q => (q.PriorityId == priorityId || (priorityId == null && q.PriorityId == null)));
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="statusId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> ByStatusId(this IQueryable<TrackerSchema.Core.Data.Tracker.Entities.TrackerTask> queryable, Guid statusId)
        {
            return queryable.Where(q => q.StatusId == statusId);
        }

        #endregion

    }
}
