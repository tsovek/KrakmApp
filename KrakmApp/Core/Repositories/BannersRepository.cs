using System.Collections.Generic;
using System.Linq;

using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class BannersRepository : Repository<Banner>, IBannersRepository
    {
        public BannersRepository(KrakmAppContext context)
            : base(context)
        { }

        public IEnumerable<Banner> GetAllByUserId(int userId)
        {
            return AllIncluding().Where(e => e.UserId == userId);
        }
    }
}
