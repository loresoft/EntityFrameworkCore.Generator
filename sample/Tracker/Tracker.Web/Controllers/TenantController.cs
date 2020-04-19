using AutoMapper;
using FluentValidation;
using Tracker.Core.Data;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    public class TenantController : EntityControllerBase<Core.Data.Entities.Tenant, TenantReadModel, TenantCreateModel, TenantUpdateModel>
    {
        public TenantController(TrackerContext dataContext, IMapper mapper, IValidator<TenantCreateModel> createValidator, IValidator<TenantUpdateModel> updateValidator)
            : base(dataContext, mapper, createValidator, updateValidator)
        {
        }
    }
}