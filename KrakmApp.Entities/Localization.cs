namespace KrakmApp.Entities
{
    public class Localization : IEntityBase
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}