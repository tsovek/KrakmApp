﻿
namespace KrakmApp.ViewModels
{
    public class MonumentViewModel
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Payable { get; set; }
        public string ImageUrl { get; set; }
    }
}
