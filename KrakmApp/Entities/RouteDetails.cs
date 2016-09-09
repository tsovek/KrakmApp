
namespace KrakmApp.Entities
{
    public class RouteDetails : IEntityBase
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int IdInType { get; set; }
        public int Order { get; set; }

        public int RouteId { get; set; }
        public virtual Route Route { get; set; }
    }
}
