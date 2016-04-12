
namespace KrakmApp.Entities
{
    public class RouteLocalization
    {
        public int LocalizationId { get; set; }
        public Localization Localization { get; set; }

        public int RouteId { get; set; }
        public Route Route { get; set; }
    }
}
