using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tracker.Core.Data.Queries;

/// <summary>
/// Provides query extension methods for <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities mapped to the <c>dbo.UserLogin</c> table.
/// </summary>
public static partial class UserLoginExtensions
{
    #region Generated Extensions
    /// <summary>
    /// Filters <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities by <c>EmailAddress</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities.</param>
    /// <param name="emailAddress">The value to match against <see cref="Tracker.Core.Data.Entities.UserLogin.EmailAddress" /> mapped to the <c>EmailAddress</c> column.</param>
    /// <returns>An <see cref="IQueryable{T}" /> of <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities matching the specified values.</returns>
    public static IQueryable<Tracker.Core.Data.Entities.UserLogin> ByEmailAddress(this IQueryable<Tracker.Core.Data.Entities.UserLogin> queryable, string emailAddress)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => q.EmailAddress == emailAddress);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.UserLogin" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities.</param>
    /// <param name="id">The value to match against <see cref="Tracker.Core.Data.Entities.UserLogin.Id" /> mapped to the <c>Id</c> column.</param>
    /// <returns>The matching <see cref="Tracker.Core.Data.Entities.UserLogin" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static Tracker.Core.Data.Entities.UserLogin? GetByKey(this IQueryable<Tracker.Core.Data.Entities.UserLogin> queryable, Guid id)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.UserLogin> dbSet)
            return dbSet.Find(id);

        return queryable.FirstOrDefault(q => q.Id == id);
    }

    /// <summary>
    /// Gets the <see cref="Tracker.Core.Data.Entities.UserLogin" /> entity matching the primary key.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities.</param>
    /// <param name="id">The value to match against <see cref="Tracker.Core.Data.Entities.UserLogin.Id" /> mapped to the <c>Id</c> column.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching <see cref="Tracker.Core.Data.Entities.UserLogin" /> entity, or <see langword="null" /> if no match is found.</returns>
    public static async System.Threading.Tasks.ValueTask<Tracker.Core.Data.Entities.UserLogin?> GetByKeyAsync(this IQueryable<Tracker.Core.Data.Entities.UserLogin> queryable, Guid id, CancellationToken cancellationToken = default)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        if (queryable is DbSet<Tracker.Core.Data.Entities.UserLogin> dbSet)
            return await dbSet.FindAsync(new object[] { id }, cancellationToken);

        return await queryable.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    /// <summary>
    /// Filters <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities by <c>UserId</c>.
    /// </summary>
    /// <param name="queryable">The source query for <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities.</param>
    /// <param name="userId">The value to match against <see cref="Tracker.Core.Data.Entities.UserLogin.UserId" /> mapped to the <c>UserId</c> column.</param>
    /// <returns>An <see cref="IQueryable{T}" /> of <see cref="Tracker.Core.Data.Entities.UserLogin" /> entities matching the specified values.</returns>
    public static IQueryable<Tracker.Core.Data.Entities.UserLogin> ByUserId(this IQueryable<Tracker.Core.Data.Entities.UserLogin> queryable, Guid? userId)
    {
        if (queryable is null)
            throw new ArgumentNullException(nameof(queryable));

        return queryable.Where(q => (q.UserId == userId || (userId == null && q.UserId == null)));
    }

    #endregion

}
