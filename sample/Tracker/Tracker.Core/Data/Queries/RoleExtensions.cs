using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="Tracker.Core.Data.Entities.Role" />.
    /// </summary>
    public static partial class RoleExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> or null if not found.</returns>
        public static Tracker.Core.Data.Entities.Role GetByKey(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.Role> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> or null if not found.</returns>
        public static ValueTask<Tracker.Core.Data.Entities.Role> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.Role> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.Core.Data.Entities.Role>(task);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="name">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> or null if not found.</returns>
        public static Tracker.Core.Data.Entities.Role GetByName(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, string name)
        {
            return queryable.FirstOrDefault(q => q.Name == name);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="name">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.Role"/> or null if not found.</returns>
        public static Task<Tracker.Core.Data.Entities.Role> GetByNameAsync(this IQueryable<Tracker.Core.Data.Entities.Role> queryable, string name)
        {
            return queryable.FirstOrDefaultAsync(q => q.Name == name);
        }

        #endregion

    }
}
