using System.Linq;
using AutoMapper;

using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Mappings
{
    public class EntityToViewModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Hotel, HotelViewModel>()
                .ForMember(
                    vm => vm.Latitude,
                    hotel => hotel.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    hotel => hotel.MapFrom(src => src.Localization.Longitude));
            Mapper.CreateMap<Partner, PartnerViewModel>()
                .ForMember(
                    vm => vm.Latitude,
                    partner => partner.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    partner => partner.MapFrom(src => src.Localization.Longitude));
            Mapper.CreateMap<Monument, MonumentViewModel>()
                .ForMember(
                    vm => vm.Latitude,
                    monument => monument.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    monument => monument.MapFrom(src => src.Localization.Longitude));
            Mapper.CreateMap<Entertainment, EntertainmentViewModel>()
                .ForMember(
                    vm => vm.Latitude,
                    entertainment => entertainment.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    entertainment => entertainment.MapFrom(src => src.Localization.Longitude));
            Mapper.CreateMap<Localization, LocalizationViewModel>();
            Mapper.CreateMap<Route, RouteViewModel>()
                .ForMember(
                    vm => vm.Localizations,
                    opt => opt.MapFrom(
                        src => src.RouteLocalization.Select(e => e.Localization)));
        }
    }
}
