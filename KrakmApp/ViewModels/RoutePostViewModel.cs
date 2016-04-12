using System.Collections.Generic;

namespace KrakmApp.ViewModels
{
    public class RoutePostViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public IEnumerable<int> LocalizationIds { get; set; }
    }
}
