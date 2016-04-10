using System.Collections.Generic;
using System.Linq;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(KrakmAppContext _context)
            : base(_context)
        { }

        public IEnumerable<Hotel> GetAllByUsername(string user)
        {
            return AllIncluding(e => e.Localization)
                .Where(e => e.User.Name == user);
        }

        public Hotel GetSingleByUsername(string user, int id)
        {
            return GetSingle(
                hotel => hotel.Id == id && hotel.User.Name == user,
                hotel => hotel.Localization);
        }
    }
}
