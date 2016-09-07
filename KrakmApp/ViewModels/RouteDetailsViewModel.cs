
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
        public string ImageUrl { get; set; }
    }

    public class RouteDetailsPostViewModel
    {
        public int RouteId { get; set; }
        public IEnumerable<RouteDetails> SpecificRoutes { get; set; }
    }

    public class RouteDetailsGetViewModel
    {
        public int Order { get; set; }
        public string ObjType { get; set; }
        public RouteDetailsViewModel Object { get; set; }
    }
}
