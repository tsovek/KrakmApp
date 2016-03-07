using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(KrakmAppContext context)
            : base(context)
        { }
    }
}
