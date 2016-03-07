using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(KrakmAppContext context)
            : base(context)
        {

        }
    }
}
