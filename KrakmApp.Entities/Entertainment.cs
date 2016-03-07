
namespace KrakmApp.Entities
{
    public class Entertainment : IEntityBase
    {
        public int Id { get; set; }
        public int EntertainmentId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Localization Localization { get; set; }
    }
}
