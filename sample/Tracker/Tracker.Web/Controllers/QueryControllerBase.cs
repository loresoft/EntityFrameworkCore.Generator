using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.Core.Data;
using Tracker.Core.Definitions;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class QueryControllerBase<TEntity, TReadModel> : ControllerBase
        where TEntity : class, IHaveIdentifier
        where TReadModel : EntityReadModel
    {
        protected QueryControllerBase(TrackerContext dataContext, IMapper mapper)
        {
            DataContext = dataContext;
            Mapper = mapper;
        }

        protected TrackerContext DataContext { get; }

        protected IMapper Mapper { get; }

        protected virtual async Task<TReadModel> ReadModel(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var model = await DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return model;
        }

        protected virtual async Task<IReadOnlyList<TReadModel>> QueryModel(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbSet = DataContext
                .Set<TEntity>();

            var query = dbSet.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);

            var results = await query
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return results;
        }
    }
}