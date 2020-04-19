using AutoMapper;
using FluentValidation;
using Tracker.Core.Data;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    public class AuditController : EntityControllerBase<Core.Data.Entities.Audit, AuditReadModel, AuditCreateModel, AuditUpdateModel>
    {
        public AuditController(TrackerContext dataContext, IMapper mapper, IValidator<AuditCreateModel> createValidator, IValidator<AuditUpdateModel> updateValidator)
            : base(dataContext, mapper, createValidator, updateValidator)
        {
        }
    }
}