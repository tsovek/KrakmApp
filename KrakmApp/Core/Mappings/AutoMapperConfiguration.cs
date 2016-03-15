using AutoMapper;

namespace KrakmApp.Core.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToViewModelProfile>();
            });
        }
    }
}
