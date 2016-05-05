using KrakmApp.Core.Repositories.Base;
using Microsoft.AspNet.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/routes/{id}")]
    public class RouteLocalizationsController : BaseController
    {
        IRouteLocalizationRepository _routeLocRepo;

        public RouteLocalizationsController(
            IRouteLocalizationRepository routeLocRepo,
            IMembershipService membershipRepo,
            ILoggingRepository loggingRepo)
            : base(membershipRepo, loggingRepo)
        {
            _routeLocRepo = routeLocRepo;
        }

        [HttpPost]
        public IActionResult Post(
            int id,
            [FromBody]int locId)
        {
            return null;
        }

        [HttpDelete("/{locId}")]
        public IActionResult Delete(
            int id,
            int locId)
        {
            return null;
        }
    }
}
