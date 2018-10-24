using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="Tracker.Core.Data.Entities.Task" />.
    /// </summary>
    public static partial class TaskExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Task"/> or null if not found.</returns>
        public static Tracker.Core.Data.Entities.Task GetByKey(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.Task> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Task"/> or null if not found.</returns>
        public static Task<Tracker.Core.Data.Entities.Task> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.Task> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="assignedId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<Tracker.Core.Data.Entities.Task> ByAssignedId(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, Guid? assignedId)
        {
            return queryable.Where(q => (q.AssignedId == assignedId || (assignedId == null && q.AssignedId == null)));
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="priorityId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<Tracker.Core.Data.Entities.Task> ByPriorityId(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, int? priorityId)
        {
            return queryable.Where(q => (q.PriorityId == priorityId || (priorityId == null && q.PriorityId == null)));
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="statusId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<Tracker.Core.Data.Entities.Task> ByStatusId(this IQueryable<Tracker.Core.Data.Entities.Task> queryable, int statusId)
        {
            return queryable.Where(q => q.StatusId == statusId);
        }

        #endregion

    }
}
