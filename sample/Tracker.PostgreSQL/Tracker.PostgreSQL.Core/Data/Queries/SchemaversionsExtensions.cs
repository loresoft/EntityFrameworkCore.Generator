using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data.Queries
{
    public static partial class SchemaversionsExtensions
    {
        #region Generated Extensions
        public static Tracker.Data.Entities.Schemaversions GetByKey(this IQueryable<Tracker.Data.Entities.Schemaversions> queryable, int schemaversionsid)
        {
            if (queryable is DbSet<Tracker.Data.Entities.Schemaversions> dbSet)
                return dbSet.Find(schemaversionsid);

            return queryable.FirstOrDefault(q => q.Schemaversionsid == schemaversionsid);
        }

        public static ValueTask<Tracker.Data.Entities.Schemaversions> GetByKeyAsync(this IQueryable<Tracker.Data.Entities.Schemaversions> queryable, int schemaversionsid)
        {
            if (queryable is DbSet<Tracker.Data.Entities.Schemaversions> dbSet)
                return dbSet.FindAsync(schemaversionsid);

            var task = queryable.FirstOrDefaultAsync(q => q.Schemaversionsid == schemaversionsid);
            return new ValueTask<Tracker.Data.Entities.Schemaversions>(task);
        }

        #endregion

    }
}
