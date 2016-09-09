using System.Collections.Generic;

namespace KrakmApp.ViewModels
{
    public enum ObjectType
    {
        None = 0,
        Partners = 1,
        Monuments = 2,
        Entertainments = 3
    }

    public class GroupObjectViewModel
    {
        public string Type { get; set; }
        public IEnumerable<SingleObjectViewModel> SingleObjects { get; set; }
    }

    public class SingleObjectViewModel
    {
        public int IdInType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
