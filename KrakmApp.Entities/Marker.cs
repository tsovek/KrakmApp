
namespace KrakmApp.Entities
{
    public class Marker : IEntityBase
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}
