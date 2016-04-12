using System.Linq;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;

namespace KrakmApp.Core.Repositories
{
    public class RouteLocalizationRepository : 
        IRouteLocalizationRepository
    {
        KrakmAppContext _context;

        public RouteLocalizationRepository(KrakmAppContext context)
        {
            _context = context;
        }

        public void Add(RouteLocalization routeLoc)
        {
            _context.Set<RouteLocalization>().Add(routeLoc);
        }

        public void Delete(RouteLocalization routeLoc)
        {
            EntityEntry dbEntityEntry = _context.Entry(routeLoc);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public bool Exists(int routeId, int locId)
        {
            return _context.Set<RouteLocalization>()
                .Any(e => e.LocalizationId == locId && 
                     e.RouteId == routeId);
                
        }
    }
}
