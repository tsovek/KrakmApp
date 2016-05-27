using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class BannersRepository : Repository<Banner>, IBannersRepository
    {
        public BannersRepository(KrakmAppContext context)
            : base(context)
        { }
    }
}
