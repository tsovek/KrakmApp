﻿using System.Collections.Generic;

using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Repositories.Base
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        void Add(HotelViewModel _hotelVM);
    }

    public interface IPartnersRepository : IRepository<Partner> { }

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

    public interface IMarkerRepository : IRepository<Marker> { }

    public interface ILocalizationRepository : IRepository<Localization> { }
}
