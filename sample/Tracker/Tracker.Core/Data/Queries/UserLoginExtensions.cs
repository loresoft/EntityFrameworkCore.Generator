using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="Tracker.Core.Data.Entities.UserLogin" />.
    /// </summary>
    public static partial class UserLoginExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="emailAddress">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<Tracker.Core.Data.Entities.UserLogin> ByEmailAddress(this IQueryable<Tracker.Core.Data.Entities.UserLogin> queryable, string emailAddress)
        {
            return queryable.Where(q => q.EmailAddress == emailAddress);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.UserLogin"/> or null if not found.</returns>
        public static Tracker.Core.Data.Entities.UserLogin GetByKey(this IQueryable<Tracker.Core.Data.Entities.UserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.UserLogin> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:Tracker.Core.Data.Entities.UserLogin"/> or null if not found.</returns>
        public static ValueTask<Tracker.Core.Data.Entities.UserLogin> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.UserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<Tracker.Core.Data.Entities.UserLogin> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Tracker.Core.Data.Entities.UserLogin>(task);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<Tracker.Core.Data.Entities.UserLogin> ByUserId(this IQueryable<Tracker.Core.Data.Entities.UserLogin> queryable, Guid? userId)
        {
            return queryable.Where(q => (q.UserId == userId || (userId == null && q.UserId == null)));
        }

        #endregion

    }
}
