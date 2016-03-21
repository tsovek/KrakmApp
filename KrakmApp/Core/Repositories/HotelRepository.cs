using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Repositories
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private ILocalizationRepository _localizationRepository;
        private IMembershipService _membershipService;

        public HotelRepository(
            ILocalizationRepository localization,
            IMembershipService membershipService,
            KrakmAppContext context)
            : base(context)
        {
            _localizationRepository = localization;
            _membershipService = membershipService;
        }

        public void Add(HotelViewModel hotelVM, ClaimsPrincipal claims)
        {
            var localization = new Localization
            {
                Latitude = hotelVM.Latitude,
                Longitude = hotelVM.Longitude,
                Default = true
            };
            _localizationRepository.Add(localization);

            User user = _membershipService.GetUserByPrinciples(claims);

            var hotel = new Hotel
            {
                User = user,
                Name = hotelVM.Name,
                Adress = hotelVM.Adress,
                Phone = hotelVM.Phone,
                Email = hotelVM.Email,
                Localizations = new Collection<Localization>() { localization }
            };

            Add(hotel);

            Commit();
        }
    }
}
