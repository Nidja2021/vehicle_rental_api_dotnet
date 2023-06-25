namespace VehicleRental.API.Models
{
    [Serializable]
    public class Reservation
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        // public Guid UserId { get; set; }

        [JsonIgnore]
        public Vehicle? Vehicle { get; set; }
        public Guid VehicleId { get; set; }
    }
}