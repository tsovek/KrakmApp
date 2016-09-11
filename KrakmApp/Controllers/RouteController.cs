using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KrakmApp.Core.Common;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Core.Services;
using KrakmApp.Entities;
using KrakmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/routes")]
    [Authorize(Policy = "OwnerOnly")]
    public class RouteController : BaseController
    {
        private IRouteRepository _routeRepository;
        private IAllRoutesFactory _allRoutesFactory;

        public RouteController(
            IRouteRepository routeRepository,
            ILoggingRepository loggingRepository,
            IMembershipService membershipService,
            IAllRoutesFactory allRoutesFactory)
            : base(membershipService, loggingRepository)
        {
            _routeRepository = routeRepository;
            _allRoutesFactory = allRoutesFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var routeVM = Enumerable.Empty<RouteViewModel>();

            try
            {
                routeVM = _allRoutesFactory.GetAllByUserId(GetUser().Id);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(routeVM);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RouteViewModel routeVM = null;

            try
            {
                Route route = _routeRepository
                    .GetSingleByUsername(id, GetUsername());
                if (route == null)
                {
                    return NotFound();
                }
                routeVM = Mapper.Map<Route, RouteViewModel>(route);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(routeVM);
        }

        [HttpPost]
        public IActionResult Post([FromBody]RoutePostViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result routeCreationResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before adding");
                }

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
                    return BadRequest();
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
                    return BadRequest();
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
