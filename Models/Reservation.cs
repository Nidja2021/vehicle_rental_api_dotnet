namespace VehicleRental.API.Models
{
    [Serializable]
    public class Reservation : BaseEntity
    {
        
        public Guid VehicleId { get; set; }
        public string? StartReservation { get; set; }
        public string? EndReservation { get; set; }

        
        [JsonIgnore] public User? User { get; set; }
        [JsonIgnore] public Vehicle? Vehicle { get; set; }
    }
}