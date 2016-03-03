
namespace KrakmApp.Entities
{
    public class Monument : IEntityBase
    {
        public int ID { get; set; }
        public int LocalizationId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Localization Localization { get; set; }
    }
}
