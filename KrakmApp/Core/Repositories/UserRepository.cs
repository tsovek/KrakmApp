using System.Collections.Generic;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        IRoleRepository _roleRepository;

        public UserRepository(
            KrakmAppContext context, 
            IRoleRepository roleRepository)
            : base (context)
        {
            _roleRepository = roleRepository;
        }

        public User GetSingleByUsername(string username)
        {
            return this.GetSingle(x => x.Name == username);
        }

        public IEnumerable<Role> GetUserRoles(string username)
        {
            List<Role> _roles = null;

            User _user = this.GetSingle(u => u.Name == username, u => u.UserRoles);
            if (_user != null)
            {
                _roles = new List<Role>();
                foreach (var _userRole in _user.UserRoles)
                    _roles.Add(_roleRepository.GetSingle(_userRole.RoleId));
            }

            return _roles;
        }
    }
}
