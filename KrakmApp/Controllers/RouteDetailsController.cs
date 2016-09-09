using System;
using System.Collections.Generic;
using System.Linq;
using KrakmApp.Core.Common;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Core.Services;
using KrakmApp.Entities;
using KrakmApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/routedetails")]
    public class RouteDetailsController : BaseController
    {
        private IRouteDetailsFactory _routeDetailsFactory;
        private IRouteDetailsRepository _routeDetailsRepository;
        private IRouteRepository _routeRepository;

        public RouteDetailsController(
            IMembershipService membership, 
            ILoggingRepository logging,
            IRouteDetailsFactory routeDetailsFactory,
            IRouteRepository routeRepository,
            IRouteDetailsRepository routeDetailsRepository) 
            : base(membership, logging)
        {
            _routeDetailsFactory = routeDetailsFactory;
            _routeRepository = routeRepository;
            _routeDetailsRepository = routeDetailsRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IEnumerable<RouteDetailsGetViewModel> pointsOfRoute = null;

            try
            {
                Route route = _routeRepository
                    .GetSingleByUsername(id, GetUsername());
                if (route == null)
                {
                    return NotFound();
                }

                pointsOfRoute = route.RouteDetails
                    .OrderBy(e => e.Order)
                    .Select(e => new RouteDetailsGetViewModel {
                        Object = _routeDetailsFactory.GetRouteDetailsViewModel(e),
                        ObjType = e.Type,
                        Order = e.Order
                    });
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(pointsOfRoute);
        }

        [HttpPost]
        public IActionResult Post([FromBody]RouteDetailsPostViewModel routeDetails)
        {
            IActionResult result = new ObjectResult(false);
            Result routeDetailsResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before saving");
                }

                if (!routeDetails.SpecificRoutes.Any())
                {
                    var res = new CodeResult(204, "Empty array");
                    return new ObjectResult(res);
                }

                Route route = _routeRepository
                    .AllIncluding(e => e.RouteDetails)
                    .FirstOrDefault(e => e.Id == routeDetails.RouteId);
                if (route == null)
                {
                    return BadRequest();
                }

                if (route.RouteDetails != null && route.RouteDetails.Any())
                {
                    List<RouteDetails> actualPoints = route.RouteDetails.ToList();
                    actualPoints.ForEach(point => _routeDetailsRepository.Delete(point));
                    route.RouteDetails.Clear();
                }

                foreach (var point in routeDetails.SpecificRoutes)
                {
                    route.RouteDetails.Add(point);
                }

                _routeRepository.Commit();

                routeDetailsResult = new Result()
                {
                    Succeeded = true,
                    Message = "Saving succeeded"
                };
            }
            catch (Exception ex)
            {
                routeDetailsResult = GetFailedResult(ex);
            }

            result = new ObjectResult(routeDetailsResult);
            return result;
        }
    }
}
