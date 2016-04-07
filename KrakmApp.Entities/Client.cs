using System;

namespace KrakmApp.Entities
{
    public class Client : IEntityBase
    {
        public int Id { get; set; }
        public int HotelId { get; set; }

        public Guid UniqueKey { get; set; }
        public string Name { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}