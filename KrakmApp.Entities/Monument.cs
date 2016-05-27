
namespace KrakmApp.Entities
{
    public class Monument : IEntityBase
    {
        public int Id { get; set; }
        public int LocalizationId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Payable { get; set; }
        public string ImageUrl { get; set; }
        public string Adress { get; set; }

        public virtual Localization Localization { get; set; }
    }
}
