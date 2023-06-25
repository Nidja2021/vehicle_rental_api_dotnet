namespace VehicleRental.API.Dtos
{
    public record UserDto(
        [Required] [StringLength(50)] string? Email
    );

    public record UserLoginDto(
        [Required] string? Email,
        [Required] string? Password
    );

    public record UserProfileDto(
        Guid Id,
        string Email,
        List<Reservation> Reservations
    );
}