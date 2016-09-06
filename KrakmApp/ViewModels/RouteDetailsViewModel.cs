
using System.Collections.Generic;
using KrakmApp.Entities;

namespace KrakmApp.ViewModels
{
    public class RouteDetailsViewModel
    {
        public string Type { get; set; }
        public int IdInType { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class RouteDetailsPostViewModel
    {
        public IEnumerable<RouteDetails> SpecificRoutes { get; set; }
    }
}
