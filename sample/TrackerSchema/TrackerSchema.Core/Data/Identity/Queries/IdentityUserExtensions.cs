using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Identity.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityUser" />.
    /// </summary>
    public static partial class IdentityUserExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUser"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="emailAddress">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUser"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Identity.Entities.IdentityUser GetByEmailAddress(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> queryable, string emailAddress)
        {
            return queryable.FirstOrDefault(q => q.EmailAddress == emailAddress);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUser"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="emailAddress">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUser"/> or null if not found.</returns>
        public static Task<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> GetByEmailAddressAsync(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> queryable, string emailAddress)
        {
            return queryable.FirstOrDefaultAsync(q => q.EmailAddress == emailAddress);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUser"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Identity.Entities.IdentityUser GetByKey(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityUser"/> or null if not found.</returns>
        public static ValueTask<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> GetByKeyAsync(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityUser> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<TrackerSchema.Core.Data.Identity.Entities.IdentityUser>(task);
        }

        #endregion

    }
}
