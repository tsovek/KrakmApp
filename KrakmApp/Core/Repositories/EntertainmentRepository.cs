using KrakmApp.Entities;
using KrakmApp.Core.Repositories.Base;

namespace KrakmApp.Core.Repositories
{
    public class EntertainmentRepository : Repository<Entertainment>, IEntertainmentRepository
    {
        public EntertainmentRepository(KrakmAppContext context)
            : base(context)
        { }
    }
}
