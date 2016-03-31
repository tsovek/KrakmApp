using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrakmApp.Entities
{
    public class Route : IEntityBase
    {
        public int Id { get; set; }
        public int HotelId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Length { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Localization> Localizations { get; set; }
    }
}
