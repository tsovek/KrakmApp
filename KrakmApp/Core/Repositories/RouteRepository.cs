using System;
using System.Collections.Generic;
using System.Linq;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;

namespace KrakmApp.Core.Repositories
{
    public class RouteRepository : Repository<Route>, IRouteRepository
    {
        public RouteRepository(KrakmAppContext context)
            : base(context)
        { }

        public IEnumerable<Route> GetAllByUsername(string username)
        {
            return AllIncludes()
                .Where(e => e.User.Name == username);
        }

        public Route GetSingleByUsername(int id, string username)
        {
            return AllIncludes().FirstOrDefault(
                e => e.Id == id && e.User.Name == username);
        }

        private IQueryable<Route> AllIncludes()
        {
            return _context.Set<Route>().Include(e => e.RouteDetails);
        }
    }

    public class RouteDetailsRepository : Repository<RouteDetails>, IRouteDetailsRepository
    {
        public RouteDetailsRepository(KrakmAppContext context)
            : base(context)
        { }
    }
}
