using AutoMapper;
using Tracker.Core.Data;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    public class UserLoginController : QueryControllerBase<Core.Data.Entities.UserLogin, UserLoginReadModel>
    {
        public UserLoginController(TrackerContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}