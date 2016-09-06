using System.Collections.Generic;

namespace KrakmApp.ViewModels
{
    public class RouteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Length { get; set; }
        public IEnumerable<RouteDetailsViewModel> RouteDetails { get; set; }
    }
}
