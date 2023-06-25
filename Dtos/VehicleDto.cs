
namespace VehicleRental.API.Dtos
{
    public record VehicleDto (
        Guid Id,
        string? Brand,
        string? Model
    );
}