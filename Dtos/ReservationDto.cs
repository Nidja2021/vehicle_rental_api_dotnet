namespace VehicleRental.API.Dtos
{
    public record ReservationDto
    (
        Guid Id,
        User User,
        Vehicle Vehicle
    );
}