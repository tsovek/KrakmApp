using System.Collections.Generic;
using System.Linq;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class PartnersRepository : Repository<Partner>, IPartnersRepository
    {
        public PartnersRepository(KrakmAppContext _context)
            : base (_context)
        { }

        public IEnumerable<Partner> GetAllByUsername(string user)
        {
            return AllIncluding(e => e.Localization)
                .Where(e => e.User.Name == user);
        }

        public Partner GetSingleByUsername(string user, int id)
        {
            return GetSingle(
                hotel => hotel.User.Name == user && hotel.Id == id,
                hotel => hotel.Localization);
        }
    }
}
