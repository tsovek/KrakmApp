using System.Collections.ObjectModel;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Repositories
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private ILocalizationRepository _localizationRepository;

        public HotelRepository(
            ILocalizationRepository localization,
            KrakmAppContext context)
            : base(context)
        {
            _localizationRepository = localization;
        }

        public void Add(HotelViewModel _hotelVM)
        {
            var localization = new Localization
            {
                Latitude = _hotelVM.Latitude,
                Longitude = _hotelVM.Longitude,
                Default = true
            };
            _localizationRepository.Add(localization);

            var hotel = new Hotel
            {
                Name = _hotelVM.Name,
                Adress = _hotelVM.Adress,
                Phone = _hotelVM.Phone,
                Localizations = new Collection<Localization>() { localization }
            };

            Add(hotel);

            Commit();
        }
    }
}
