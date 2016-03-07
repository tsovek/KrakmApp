using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class MonumentsRepository : Repository<Monument>, IMonumentRepository
    {
        public MonumentsRepository(KrakmAppContext context)
            : base(context)
        { }
    }
}
