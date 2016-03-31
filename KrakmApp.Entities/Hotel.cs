using System.Collections.Generic;

namespace KrakmApp.Entities
{
    public class Hotel : IEntityBase
    {
        public Hotel()
        {
            Localizations = new List<Localization>();
            Clients = new List<Client>();
            Partners = new List<HotelsPartners>();
            Routes = new List<Route>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Localization> Localizations { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<HotelsPartners> Partners { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
