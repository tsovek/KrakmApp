using AutoMapper;

using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Mappings
{
    public class EntityToViewModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Hotel, HotelViewModel>();
            Mapper.CreateMap<Partner, PartnerViewModel>();
        }
    }
}
