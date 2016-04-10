using System.Collections.Generic;
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
        }
    }
}
