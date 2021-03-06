﻿using System.Collections.Generic;
using AutoMapper;

using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Mappings
{
    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
            CreateMap<Hotel, HotelViewModel>()
                .ForMember(
                    vm => vm.Latitude,
                    hotel => hotel.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    hotel => hotel.MapFrom(src => src.Localization.Longitude));
            CreateMap<Partner, PartnerViewModel>()
                .ForMember(
                    vm => vm.Latitude,
                    partner => partner.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    partner => partner.MapFrom(src => src.Localization.Longitude))
                .ForMember(
                    vm => vm.StartPromotion,
                    partner => partner.MapFrom(src => src.StartPromotion))
                .ForMember(
                    vm => vm.EndPromotion,
                    partner => partner.MapFrom(src => src.EndPromotion));
            CreateMap<Monument, MonumentViewModel>()
                .ForMember(
                    vm => vm.Latitude,
                    monument => monument.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    monument => monument.MapFrom(src => src.Localization.Longitude));
            CreateMap<Entertainment, EntertainmentViewModel>()
                .ForMember(
                    vm => vm.Latitude,
                    entertainment => entertainment.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    entertainment => entertainment.MapFrom(src => src.Localization.Longitude));
            CreateMap<Localization, LocalizationViewModel>();
            CreateMap<RouteDetails, RouteDetailsViewModel>();
            CreateMap<Route, RouteViewModel>()
                .ForMember(
                    vm => vm.RouteDetails,
                    opt => opt.MapFrom(src => src.RouteDetails));
            CreateMap<Banner, BannerViewModel>()
                .ForMember(
                    vm => vm.StartPromotion,
                    banner => banner.MapFrom(src => src.Start))
                .ForMember(
                    vm => vm.EndPromotion,
                    banner => banner.MapFrom(src => src.End))
                .ForMember(
                    vm => vm.ImageUrl,
                    banner => banner.MapFrom(src => src.PhotoUrl));
            CreateMap<Client, ClientViewModel>();
            CreateMap<Entertainment, SingleObjectViewModel>()
                .ForMember(
                    vm => vm.IdInType,
                    entertainment => entertainment.MapFrom(src => src.Id))
                .ForMember(
                    vm => vm.Latitude,
                    entertainment => entertainment.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    entertainment => entertainment.MapFrom(src => src.Localization.Longitude));
            CreateMap<Monument, SingleObjectViewModel>()
                .ForMember(
                    vm => vm.IdInType,
                    monument => monument.MapFrom(src => src.Id))
                .ForMember(
                    vm => vm.Latitude,
                    monument => monument.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    monument => monument.MapFrom(src => src.Localization.Longitude));
            CreateMap<Partner, SingleObjectViewModel>()
                .ForMember(
                    vm => vm.IdInType,
                    partner => partner.MapFrom(src => src.Id))
                .ForMember(
                    vm => vm.Latitude,
                    partner => partner.MapFrom(src => src.Localization.Latitude))
                .ForMember(
                    vm => vm.Longitude,
                    partner => partner.MapFrom(src => src.Localization.Longitude));
        }
    }
}
