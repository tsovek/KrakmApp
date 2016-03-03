
namespace KrakmApp.Entities
{
    public class Entertainment : IEntityBase
    {
        public int ID { get; set; }
        public int EntertainmentId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Localization Localization { get; set; }
    }
}
