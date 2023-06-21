
namespace VehicleRental.API.Dtos
{
    public record VehicleDto (
        string? Brand,
        string? Model,
        int Year,
        string? FuelType,
        decimal RentalRate
    );
}