

namespace VehicleRental.API.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public Reservation? Reservation { get; set; }
    }
}