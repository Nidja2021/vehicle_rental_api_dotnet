namespace VehicleRental.API.Services.VehicleService
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetVehicles();
        Task<VehicleDto> GetVehicle(Guid id);
        Task<Guid> AddVehicle(Vehicle vehicleRequest);
        Task<VehicleDto> UpdateVehicle(Guid id, VehicleDto vehicleRequest);
        Task<string> DeleteVehicle(Guid id);

    }
}