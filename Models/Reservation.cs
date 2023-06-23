namespace VehicleRental.API.Models
{
    
    public class Reservation
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public Guid UserId { get; set; }
        public Vehicle? Vehicle { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime StartReservation { get; set; } = DateTime.UtcNow;
        public DateTime EndReservation { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = false;
    }
}