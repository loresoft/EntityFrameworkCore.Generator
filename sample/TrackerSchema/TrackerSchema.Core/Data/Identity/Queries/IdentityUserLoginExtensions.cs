using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Identity.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin" />.
    /// </summary>
    public static partial class IdentityUserLoginExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="emailAddress">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> ByEmailAddress(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> queryable, string emailAddress)
        {
            return queryable.Where(q => q.EmailAddress == emailAddress);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin GetByKey(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin"/> or null if not found.</returns>
        public static ValueTask<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> GetByKeyAsync(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin>(task);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="userId">The value to filter by.</param>
        /// <returns>An <see cref="T: System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified.</returns>
        public static IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> ByUserId(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUserLogin> queryable, Guid? userId)
        {
            return queryable.Where(q => (q.UserId == userId || (userId == null && q.UserId == null)));
        }

        #endregion

    }
}
