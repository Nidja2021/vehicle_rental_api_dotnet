

namespace VehicleRental.API.Models
{
    public class Vehicle : BaseEntity
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public string? Color { get; set; }
        public bool Availability { get; set; } = true;

        public Reservation? Reservation { get; set; }
    }
}