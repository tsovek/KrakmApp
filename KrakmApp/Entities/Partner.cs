using System;
using System.Collections.Generic;

namespace KrakmApp.Entities
{
    public class Partner : IEntityBase
    {
        public int Id { get; set; }
        public int LocalizationId { get; set; }
        public int UserId { get; set; }

        public Guid UniqueKey { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public decimal Commission { get; set; }
        public DateTime StartPromotion { get; set; }
        public DateTime EndPromotion { get; set; }
        public int PromotionKind { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public virtual Localization Localization { get; set; }
        public virtual User User { get; set; }
    }
}