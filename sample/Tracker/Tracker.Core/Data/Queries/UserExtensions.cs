using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="Tracker.Core.Data.Entities.User" />.
    /// </summary>
    public static partial class UserExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.User"/> or null if not found.</returns>
        public static Tracker.Core.Data.Entities.User GetByKey(this IQueryable<Tracker.Core.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.User> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.User"/> or null if not found.</returns>
        public static Task<Tracker.Core.Data.Entities.User> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.User> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.User> dbSet)
                return dbSet.FindAsync(id);

            return queryable.FirstOrDefaultAsync(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:Tracker.Core.Data.Entities.User"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="emailAddress">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.User"/> or null if not found.</returns>
        public static Tracker.Core.Data.Entities.User GetByEmailAddress(this IQueryable<Tracker.Core.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefault(q => q.EmailAddress == emailAddress);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:Tracker.Core.Data.Entities.User"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="emailAddress">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.User"/> or null if not found.</returns>
        public static Task<Tracker.Core.Data.Entities.User> GetByEmailAddressAsync(this IQueryable<Tracker.Core.Data.Entities.User> queryable, string emailAddress)
        {
            return queryable.FirstOrDefaultAsync(q => q.EmailAddress == emailAddress);
        }

        #endregion

    }
}
