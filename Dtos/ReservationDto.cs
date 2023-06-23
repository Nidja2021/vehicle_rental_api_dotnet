namespace VehicleRental.API.Dtos
{
    public record ReservationDto
    (
        Guid Id,
        User User,
        Vehicle Vehicle,
        DateTime CreatedAt,
        DateTime StartReservation,
        DateTime EndReservation,
        bool Active
    );
}