
using System;

namespace KrakmApp.ViewModels
{
    public class PartnerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public decimal Commission { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartPromotion { get; set; }
        public DateTime EndPromotion { get; set; }
    }
}
