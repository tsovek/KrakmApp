using System.Collections.Generic;

namespace KrakmApp.ViewModels
{
    public class MobileObjectsViewModel
    {
        public ObjectsViewModel Objects { get; set; }
        public AllRoutesViewModel AllRoutes { get; set; }
        public AllBannersViewModel AllBanners { get; set; }
        public ClientViewModel Client { get; set; }
        public HotelViewModel Hotel { get; set; }
    }

    public class AllRoutesViewModel
    {
        public IEnumerable<RouteViewModel> Routes { get; set; }
    }

    public class AllBannersViewModel
    {
        public IEnumerable<BannerViewModel> Banners { get; set; }
    }
}
