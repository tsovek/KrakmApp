using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories.Base
{
    public class MarkerRepository : Repository<Marker>, IMarkerRepository
    {
        public MarkerRepository(KrakmAppContext context)
            : base(context)
        { }
    }
}
