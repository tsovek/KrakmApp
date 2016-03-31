namespace KrakmApp.Entities
{
    public class Localization : IEntityBase
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Default { get; set; }
        public bool Active { get; set; }
        public LocalizationType LocalizationType { get; set; }
    }

    public enum LocalizationType
    {
        Kids = 0,
        Students = 1
    }
}