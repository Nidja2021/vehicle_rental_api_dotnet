namespace VehicleRental.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required] public string? Email { get; set; }
        [Required] [JsonIgnore] public string? Password { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}