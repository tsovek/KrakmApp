using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Services
{
    public interface IAllRoutesFactory
    {
        IEnumerable<RouteViewModel> GetAllByUserId(int userId);
    }

    public class AllRoutesFactory : IAllRoutesFactory
    {
        IRouteRepository _routeRepository;
        IRouteDetailsFactory _routeDetailsFactory;

        public AllRoutesFactory(
            IRouteRepository routesRepo, 
            IRouteDetailsFactory routeDetailsFactory)
        {
            _routeRepository = routesRepo;
            _routeDetailsFactory = routeDetailsFactory;
        }

        public IEnumerable<RouteViewModel> GetAllByUserId(int userId)
        {
            IEnumerable<RouteViewModel> allRoutes = null;
            IEnumerable<Route> nonSortedRoutes = _routeRepository
                    .AllIncluding(r => r.RouteDetails)
                    .Where(r => r.UserId == userId);
            IEnumerable<Route> routes = nonSortedRoutes.Select(e => new Route()
            {
                Active = e.Active,
                Description = e.Description,
                User = e.User,
                UserId = e.UserId,
                Id = e.Id,
                Length = e.Length,
                Name = e.Name,
                RouteDetails = e.RouteDetails.OrderBy(det => det.Order).ToList()
            });
            allRoutes = Mapper.Map<
                IEnumerable<Route>,
                IEnumerable<RouteViewModel>>(routes, opt => opt.AfterMap(
                    (routesModel, routesVM) =>
                    {
                        var routeDetailsViewModels =
                            routesVM.SelectMany(e => e.RouteDetails);
                        foreach (var routeDetails in routeDetailsViewModels)
                        {
                            _routeDetailsFactory
                                .CompleteParamsAfterMapping(routeDetails);
                        }
                    }));

            return allRoutes;
        }
    }
}
