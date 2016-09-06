using System;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Services
{
    public interface IRouteDetailsFactory
    {
        void CompleteParamsAfterMapping(RouteDetailsViewModel routeDetailsVM);
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

        public void CompleteParamsAfterMapping(RouteDetailsViewModel singleRoute)
        {
            var idType = new IdTypePair()
            {
                Id = singleRoute.IdInType,
                Type = singleRoute.Type
            };
            var nameDesc = this.GetNameAndDescriptionByIdAndType(idType);
            singleRoute.Name = nameDesc.Name;
            singleRoute.Description = nameDesc.Description;
            singleRoute.Longitude = nameDesc.Longitude;
            singleRoute.Latitude = nameDesc.Latitude;
        }

        private NameDescriptionPair GetNameAndDescriptionByIdAndType(
            IdTypePair type)
        {
            switch (type.Type)
            {
                case "Entertainment":
                    var entertainment = _entertainments.GetSingle(type.Id);
                    return new NameDescriptionPair()
                    {
                        Name = entertainment.Name,
                        Description = entertainment.Description,
                        Longitude = entertainment.Localization.Longitude,
                        Latitude = entertainment.Localization.Latitude
                    };
                case "Monument":
                    var monument = _monuments.GetSingle(type.Id);
                    return new NameDescriptionPair()
                    {
                        Name = monument.Name,
                        Description = monument.Description,
                        Longitude = monument.Localization.Longitude,
                        Latitude = monument.Localization.Latitude
                    };
                case "Partner":
                    var partner = _partners.GetSingle(type.Id);
                    return new NameDescriptionPair()
                    {
                        Name = partner.Name,
                        Description = partner.Description,
                        Longitude = partner.Localization.Longitude,
                        Latitude = partner.Localization.Latitude
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
        }
    }
}
