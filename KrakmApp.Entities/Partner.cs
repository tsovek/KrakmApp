using System;

namespace KrakmApp.Entities
{
    public class Partner : IEntityBase
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int MarkerId { get; set; }
        
        public Guid UniqueKey { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }

        public virtual Localization Localization { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Marker Marker { get; set; }
    }
}