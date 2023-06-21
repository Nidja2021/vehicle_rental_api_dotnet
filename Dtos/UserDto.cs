namespace VehicleRental.API.Dtos
{
    public record UserDto(
        [Required] [StringLength(50)] string? Username,
        [Required] [StringLength(50)] string? Email,
        string? FullName
    );

    public record UserLoginDto(
        [Required] string? Email,
        [Required] string? Password
    );
}