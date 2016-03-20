
namespace KrakmApp.Entities
{
    public class HotelsPartners
    {
        public int HotelId { get; set; }
        public int PartnerId { get; set; }

        public Hotel Hotel { get; set; }
        public Partner Partner { get; set; }
    }
}
