using System.Collections.Generic;

namespace KrakmApp.Entities
{
    public class Hotel : IEntityBase
    {
        public Hotel()
        {
            Clients = new List<Client>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int LocalizationId { get; set; }

        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual User User { get; set; }
        public virtual Localization Localization { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
