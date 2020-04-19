using AutoMapper;
using FluentValidation;
using Tracker.Core.Data;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    public class TaskController : EntityControllerBase<Core.Data.Entities.Task, TaskReadModel, TaskCreateModel, TaskUpdateModel>
    {
        public TaskController(TrackerContext dataContext, IMapper mapper, IValidator<TaskCreateModel> createValidator, IValidator<TaskUpdateModel> updateValidator)
            : base(dataContext, mapper, createValidator, updateValidator)
        {
        }
    }
}
