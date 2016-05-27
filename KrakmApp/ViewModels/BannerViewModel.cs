using System;

namespace KrakmApp.ViewModels
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartPromotion { get; set; }
        public DateTime EndPromotion { get; set; }
        public string ImageUrl { get; set; }
    }
}
