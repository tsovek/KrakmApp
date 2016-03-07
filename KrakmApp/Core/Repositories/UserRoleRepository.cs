using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(KrakmAppContext context)
            : base(context)
        {

        }
    }
}
