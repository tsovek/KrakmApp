using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(KrakmAppContext context)
            : base(context)
        { }
    }
}
