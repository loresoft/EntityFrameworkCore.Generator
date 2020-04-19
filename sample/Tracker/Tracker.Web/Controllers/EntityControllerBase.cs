using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tracker.Core.Data;
using Tracker.Core.Definitions;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class EntityControllerBase<TEntity, TReadModel, TCreateModel, TUpdateModel> : QueryControllerBase<TEntity, TReadModel>
        where TEntity : class, IHaveIdentifier
        where TReadModel : EntityReadModel
        where TCreateModel : EntityCreateModel
        where TUpdateModel : EntityUpdateModel

    {
        protected EntityControllerBase(TrackerContext dataContext, IMapper mapper, IValidator<TCreateModel> createValidator, IValidator<TUpdateModel> updateValidator) : base(dataContext, mapper)
        {
            CreateValidator = createValidator;
            UpdateValidator = updateValidator;
        }
        
        protected IValidator<TCreateModel> CreateValidator { get; }

        protected IValidator<TUpdateModel> UpdateValidator { get; }

        [HttpPost("")]
        public virtual async Task<ActionResult<TReadModel>> Create(CancellationToken cancellationToken, TCreateModel createModel)
        {
            var readModel = await CreateModel(createModel, cancellationToken);

            return readModel;
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TReadModel>> Update(CancellationToken cancellationToken, Guid id, TUpdateModel updateModel)
        {
            var readModel = await UpdateModel(id, updateModel, cancellationToken);
            if (readModel == null)
                return NotFound();

            return readModel;
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TReadModel>> Delete(CancellationToken cancellationToken, Guid id)
        {
            var readModel = await DeleteModel(id, cancellationToken);
            if (readModel == null)
                return NotFound();

            return readModel;
        }


        protected virtual async Task<TReadModel> CreateModel(TCreateModel createModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            var identityName = User?.Identity?.Name;

            createModel.Created = DateTimeOffset.UtcNow;
            createModel.CreatedBy = identityName;
            createModel.Updated = DateTimeOffset.UtcNow;
            createModel.UpdatedBy = identityName;

            // validate model
            await CreateValidator.ValidateAndThrowAsync(createModel, cancellationToken: cancellationToken);

            // create new entity from model
            var entity = Mapper.Map<TEntity>(createModel);

            // add to data set, id should be generated
            await DataContext
                .Set<TEntity>()
                .AddAsync(entity, cancellationToken);

            // save to database
            await DataContext
                .SaveChangesAsync(cancellationToken);

            // convert to read model
            var readModel = await ReadModel(entity.Id, cancellationToken);

            return readModel;
        }

        protected virtual async Task<TReadModel> UpdateModel(Guid id, TUpdateModel updateModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            var identityName = User?.Identity?.Name;

            updateModel.Updated = DateTimeOffset.UtcNow;
            updateModel.UpdatedBy = identityName;

            // validate model
            await UpdateValidator.ValidateAndThrowAsync(updateModel, cancellationToken: cancellationToken);

            // primary key
            var keyValue = new object[] { id };

            // find entity to update by id, not model id
            var entity = await DataContext
                .Set<TEntity>()
                .FindAsync(keyValue, cancellationToken);

            if (entity == null)
                return default(TReadModel);

            // copy updates from model to entity
            Mapper.Map(updateModel, entity);

            // save updates
            await DataContext
                .SaveChangesAsync(cancellationToken);

            // return read model
            var readModel = await ReadModel(id, cancellationToken);
            return readModel;
        }

        protected virtual async Task<TReadModel> DeleteModel(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbSet = DataContext
                .Set<TEntity>();

            var keyValue = new object[] { id };

            // find entity to delete by id
            var entity = await dbSet
                .FindAsync(keyValue, cancellationToken);

            if (entity == null)
                return default(TReadModel);

            // return read model
            var readModel = await ReadModel(id, cancellationToken);

            // delete entry
            dbSet.Remove(entity);

            // save
            await DataContext
                .SaveChangesAsync(cancellationToken);

            return readModel;
        }
    }
}