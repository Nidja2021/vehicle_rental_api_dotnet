namespace VehicleRental.API.Models
{
    public class User : BaseEntity
    {
        [Required] public string? Email { get; set; }
        [Required] public string? Password { get; set; }
        public Role Role { get; set; } = Role.USER;

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}