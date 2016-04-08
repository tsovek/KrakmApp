using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Repositories
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(KrakmAppContext _context)
            : base(_context)
        { }
    }
}
