using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KrakmApp.Core.Common;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/routes")]
    [Authorize(Policy = "OwnerOnly")]
    public class RouteController : BaseController
    {
        private IRouteRepository _routeRepository;
        private ILocalizationRepository _localizationRepository;
        private IRouteLocalizationRepository _routeLocalizationRepo;

        public RouteController(
            IRouteRepository routeRepository,
            ILocalizationRepository localizationRepository,
            ILoggingRepository loggingRepository,
            IMembershipService membershipService,
            IRouteLocalizationRepository routeLocalizationRepo)
            : base(membershipService, loggingRepository)
        {
            _routeRepository = routeRepository;
            _localizationRepository = localizationRepository;
            _routeLocalizationRepo = routeLocalizationRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var routeVM = Enumerable.Empty<RouteViewModel>();

            try
            {
                IEnumerable<Route> routes = _routeRepository
                    .GetAllByUsername(GetUsername());
                routeVM = Mapper.Map<
                    IEnumerable<Route>,
                    IEnumerable<RouteViewModel>>(routes);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(routeVM);
        }

        [HttpGet("{id}")]
        public IActionResult Get(
            int id)
        {
            RouteViewModel routeVM = null;

            try
            {
                Route route = _routeRepository
                    .GetSingleByUsername(id, GetUsername());
                routeVM = Mapper.Map<Route, RouteViewModel>(route);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(routeVM);
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody]RoutePostViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result routeCreationResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before adding");
                }

                IEnumerable<Localization> localizations = value.LocalizationIds
                    .Select(loc => _localizationRepository.GetSingle(loc));

                User user = GetUser();
                var route = new Route
                {
                    Name = value.Name,
                    Description = value.Description,
                    Active = value.Active,
                    User = user,
                    Length = 0
                };
                _routeRepository.Add(route);
                _routeRepository.Commit();

                routeCreationResult = new Result()
                {
                    Succeeded = true,
                    Message = "Adding succeeded"
                };
            }
            catch (Exception ex)
            {
                routeCreationResult = GetFailedResult(ex);
            }

            result = new ObjectResult(routeCreationResult);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(
            int id, 
            [FromBody]RoutePostViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result routeEditionResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before adding");
                }

                Route route = _routeRepository
                    .GetSingleByUsername(id, GetUsername());

                if (route == null)
                {
                    return HttpBadRequest();
                }

                route.Name = value.Name;
                route.Description = value.Description;
                route.Active = value.Active;
                _routeRepository.Edit(route);

                _routeRepository.Commit();

                routeEditionResult = new Result()
                {
                    Succeeded = true,
                    Message = "Editing succeeded"
                };
            }
            catch (Exception ex)
            {
                routeEditionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(routeEditionResult);
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id)
        {
            IActionResult result = new ObjectResult(false);
            Result routeDeletionResult = null;

            try
            {
                Route route = _routeRepository
                    .GetSingleByUsername(id, GetUsername());
                if (route != null)
                {
                    _routeRepository.Delete(route);
                    _routeRepository.Commit();

                    routeDeletionResult = new Result()
                    {
                        Succeeded = true,
                        Message = "Deletion succeeded"
                    };
                }
                else
                {
                    return HttpBadRequest();
                }
            }
            catch (Exception ex)
            {
                routeDeletionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(routeDeletionResult);
            return result;
        }
    }
}
