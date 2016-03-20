using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class LocalizationRepository : 
        Repository<Localization>, ILocalizationRepository
    {
        public LocalizationRepository(KrakmAppContext context)
            : base(context)
        { }
    }
}
