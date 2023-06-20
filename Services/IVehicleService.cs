namespace VehicleRental.API.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetVehicles();
        Task<VehicleDto> GetVehicle(Guid id);
        Task<Guid> AddVehicle(VehicleDto vehicleRequest);
        Task<VehicleDto> UpdateVehicle(Guid id, VehicleDto vehicleRequest);
        Task<string> DeleteVehicle(Guid id);

    }
}