using AutoMapper;
using FluentValidation;
using Tracker.Core.Data;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    public class UserController : EntityControllerBase<Core.Data.Entities.User, UserReadModel, UserCreateModel, UserUpdateModel>
    {
        public UserController(TrackerContext dataContext, IMapper mapper, IValidator<UserCreateModel> createValidator, IValidator<UserUpdateModel> updateValidator)
            : base(dataContext, mapper, createValidator, updateValidator)
        {
        }
    }
}