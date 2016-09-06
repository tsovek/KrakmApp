using System.Collections.Generic;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Core.Services;
using Microsoft.AspNet.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/routedetails")]
    public class RouteDetailsController : BaseController
    {
        private IRouteDetailsFactory _routeDetailsFactory;

        public RouteDetailsController(
            IMembershipService membership, 
            ILoggingRepository logging,
            IRouteDetailsFactory routeDetailsFactory) 
            : base(membership, logging)
        {
            _routeDetailsFactory = routeDetailsFactory;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
