using System.Collections.Generic;

namespace KrakmApp.Entities
{
    public class Hotel : IEntityBase
    {
        public Hotel()
        {
            Localizations = new List<Localization>();
            Clients = new List<Client>();
            Partners = new List<Partner>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Localization> Localizations { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
    }
}
