using AutoMapper;
using FluentValidation;
using Tracker.Core.Data;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    public class RoleController : EntityControllerBase<Core.Data.Entities.Role, RoleReadModel, RoleCreateModel, RoleUpdateModel>
    {
        public RoleController(TrackerContext dataContext, IMapper mapper, IValidator<RoleCreateModel> createValidator, IValidator<RoleUpdateModel> updateValidator)
            : base(dataContext, mapper, createValidator, updateValidator)
        {
        }
    }
}