using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class PartnersRepository : Repository<Partner>, IPartnersRepository
    {
        public PartnersRepository(KrakmAppContext _context)
            : base (_context)
        { }
    }
}
