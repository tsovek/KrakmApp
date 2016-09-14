using System.Linq;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Services
{
    public interface IRouteDetailsFactory
    {
        void CompleteParamsAfterMapping(RouteDetailsViewModel routeDetailsVM);
        RouteDetailsViewModel GetRouteDetailsViewModel(RouteDetails routeDetails);
    }

    public class RouteDetailsFactory : IRouteDetailsFactory
    {
        private IEntertainmentRepository _entertainments;
        private IMonumentRepository _monuments;
        private IPartnersRepository _partners;

        public RouteDetailsFactory(
            IEntertainmentRepository entertainments,
            IMonumentRepository monuments,
            IPartnersRepository partners)
        {
            _entertainments = entertainments;
            _monuments = monuments;
            _partners = partners;
        }

        public RouteDetailsViewModel GetRouteDetailsViewModel(RouteDetails routeDetails)
        {
            var viewModel = new RouteDetailsViewModel();
            var idTypePair = new IdTypePair
            {
                Id = routeDetails.IdInType,
                Type = routeDetails.Type
            };

            viewModel.Order = routeDetails.Order;
            viewModel.IdInType = routeDetails.IdInType;
            viewModel.Type = routeDetails.Type;
            CompleteParamsAfterMapping(viewModel);

            return viewModel;
        }

        public void CompleteParamsAfterMapping(RouteDetailsViewModel singleRoute)
        {
            var idType = new IdTypePair()
            {
                Id = singleRoute.IdInType,
                Type = singleRoute.Type
            };
            var nameDesc = GetNameAndDescriptionByIdAndType(idType);
            singleRoute.Name = nameDesc.Name;
            singleRoute.Description = nameDesc.Description;
            singleRoute.Longitude = nameDesc.Longitude;
            singleRoute.Latitude = nameDesc.Latitude;
            singleRoute.ImageUrl = nameDesc.ImageUrl;
        }

        private NameDescriptionPair GetNameAndDescriptionByIdAndType(IdTypePair type)
        {
            switch (type.Type)
            {
                case "Entertainments":
                    var entertainment = _entertainments
                        .AllIncluding(e => e.Localization)
                        .First(e => e.Id == type.Id);
                    return new NameDescriptionPair()
                    {
                        Name = entertainment.Name,
                        Description = entertainment.Description,
                        Longitude = entertainment.Localization.Longitude,
                        Latitude = entertainment.Localization.Latitude,
                        ImageUrl = entertainment.ImageUrl
                    };
                case "Monuments":
                    var monument = _monuments
                        .AllIncluding(e => e.Localization)
                        .First(e => e.Id == type.Id);
                    return new NameDescriptionPair()
                    {
                        Name = monument.Name,
                        Description = monument.Description,
                        Longitude = monument.Localization.Longitude,
                        Latitude = monument.Localization.Latitude,
                        ImageUrl = monument.ImageUrl
                    };
                case "Partners":
                    var partner = _partners
                        .AllIncluding(e => e.Localization)
                        .First(e => e.Id == type.Id);
                    return new NameDescriptionPair()
                    {
                        Name = partner.Name,
                        Description = partner.Description,
                        Longitude = partner.Localization.Longitude,
                        Latitude = partner.Localization.Latitude,
                        ImageUrl = partner.ImageUrl
                    };
                default:
                    break;
            }
            return null;
        }

        class IdTypePair
        {
            public int Id { get; set; }
            public string Type { get; set; }
        }

        class NameDescriptionPair
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
