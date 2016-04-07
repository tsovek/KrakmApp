using System;

namespace KrakmApp.Entities
{
    public class Entertainment : IEntityBase
    {
        public int Id { get; set; }
        public int LocalizationId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Payable { get; set; }

        public virtual Localization Localization { get; set; }
        public virtual User User { get; set; }
    }
}
