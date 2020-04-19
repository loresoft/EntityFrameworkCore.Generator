using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="Tracker.Core.Data.Entities.Audit" />.
    /// </summary>
    public static partial class AuditExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Audit"/> or null if not found.</returns>
        public static Tracker.Core.Data.Entities.Audit GetByKey(this IQueryable<Tracker.Core.Data.Entities.Audit> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.Audit> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Audit"/> or null if not found.</returns>
        public static ValueTask<Tracker.Core.Data.Entities.Audit> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.Audit> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.Audit> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.Core.Data.Entities.Audit>(task);
        }

        #endregion

    }
}
