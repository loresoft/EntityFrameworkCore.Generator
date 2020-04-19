using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrackerSchema.Core.Data.Identity.Queries
{
    /// <summary>
    /// Query extensions for entity <see cref="TrackerSchema.Core.Data.Identity.Entities.IdentityRole" />.
    /// </summary>
    public static partial class IdentityRoleExtensions
    {
        #region Generated Extensions
        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityRole"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Identity.Entities.IdentityRole GetByKey(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Gets an instance by the primary key.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="id">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityRole"/> or null if not found.</returns>
        public static ValueTask<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> GetByKeyAsync(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> queryable, Guid id)
        {
            if (queryable is DbSet<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<TrackerSchema.Core.Data.Identity.Entities.IdentityRole>(task);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityRole"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="name">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityRole"/> or null if not found.</returns>
        public static TrackerSchema.Core.Data.Identity.Entities.IdentityRole GetByName(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> queryable, string name)
        {
            return queryable.FirstOrDefault(q => q.Name == name);
        }

        /// <summary>
        /// Gets an instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityRole"/> by using a unique index.
        /// </summary>
        /// <param name="queryable">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
        /// <param name="name">The value to filter by.</param>
        /// <returns>An instance of <see cref="T:TrackerSchema.Core.Data.Identity.Entities.IdentityRole"/> or null if not found.</returns>
        public static Task<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> GetByNameAsync(this IQueryable<TrackerSchema.Core.Data.Identity.Entities.IdentityRole> queryable, string name)
        {
            return queryable.FirstOrDefaultAsync(q => q.Name == name);
        }

        #endregion

    }
}
