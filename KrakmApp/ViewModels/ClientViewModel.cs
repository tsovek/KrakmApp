using System;

namespace KrakmApp.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool Activated { get; set; }
        public string UniqueKey { get; set; }
        public int HotelId { get; set; }
    }
}
