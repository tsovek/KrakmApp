using System.Collections.Generic;

namespace KrakmApp.Entities
{
    public class Route : IEntityBase
    {
        public Route()
        {
            RouteDetails = new List<RouteDetails>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Length { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<RouteDetails> RouteDetails { get; set; }
    }
}
