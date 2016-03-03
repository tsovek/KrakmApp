using System;

namespace KrakmApp.Entities
{
    public class Partner : IEntityBase
    {
        public int ID { get; set; }
        public int LocalizationId { get; set; }
        public int HotelId { get; set; }
        
        public Guid UniqueKey { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public virtual Localization Localization { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}