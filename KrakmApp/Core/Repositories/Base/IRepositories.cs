using System.Collections.Generic;
using System.Threading.Tasks;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories.Base
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        IEnumerable<Hotel> GetAllByUsername(string user);
        Hotel GetSingleByUsername(string user, int id);
    }

    public interface IPartnersRepository : IRepository<Partner>
    {
        IEnumerable<Partner> GetAllByUsername(string user);
        Partner GetSingleByUsername(string user, int id);
    }

    public interface IClientRepository : IRepository<Client> { }

    public interface IMonumentRepository : IRepository<Monument> { }

    public interface IEntertainmentRepository : IRepository<Entertainment> { }

    public interface IRoleRepository : IRepository<Role> { }

    public interface IUserRepository : IRepository<User>
    {
        User GetSingleByUsername(string username);
        IEnumerable<Role> GetUserRoles(string username);
    }

    public interface IUserRoleRepository : IRepository<UserRole> { }

    public interface ILoggingRepository : IRepository<Error> { }

    public interface ILocalizationRepository : IRepository<Localization> { }
}
