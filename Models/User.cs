namespace VehicleRental.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required] public string? Username { get; set; }
        [Required] public string? Email { get; set; }
        [Required] public string? Password { get; set; }
        public string? FullName { get; set; } = "";
        public RoleEnum Role { get; set; } = RoleEnum.USER;
        public List<Reservation?> Reservations { get; set; } = new List<Reservation?>();
    }
}