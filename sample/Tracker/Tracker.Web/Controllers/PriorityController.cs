using AutoMapper;
using Tracker.Core.Data;
using Tracker.Core.Domain.Models;

namespace Tracker.Web.Controllers
{
    public class PriorityController : QueryControllerBase<Core.Data.Entities.Priority, PriorityReadModel>
    {
        public PriorityController(TrackerContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}