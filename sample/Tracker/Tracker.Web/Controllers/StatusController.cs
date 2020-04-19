using AutoMapper;
using Tracker.Core.Data;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    public class StatusController : QueryControllerBase<Core.Data.Entities.Status, StatusReadModel>
    {
        public StatusController(TrackerContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}