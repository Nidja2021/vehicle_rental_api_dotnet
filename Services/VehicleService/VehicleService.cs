using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VehicleService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VehicleDto>> GetVehicles()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            var vehiclesResponse = vehicles.Select(vehicle => _mapper.Map<VehicleDto>(vehicle)).ToList();
            return vehiclesResponse;
        }

        public async Task<Guid> AddVehicle(VehicleDto vehicleRequest)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleRequest);
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return vehicle.Id;
        }

        public async Task<VehicleDto> GetVehicle(Guid id)
        {
            var vehicle = await isVehicleExisted(id);
            if (vehicle == null) throw new Exception("Vehicle does not exists.");
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
            return vehicleDto;
        }

        public async Task<VehicleDto> UpdateVehicle(Guid id, VehicleDto vehicleRequest)
        {
            var vehicle = await isVehicleExisted(id);
            if (vehicle == null) throw new Exception("Vehicle does not exists.");
            vehicle.Brand = vehicleRequest.Brand;
            vehicle.Model = vehicleRequest.Model;
            vehicle.Year = vehicleRequest.Year;
            vehicle.FuelType = vehicleRequest.FuelType;
            vehicle.RentalRate = vehicleRequest.RentalRate;

            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<string> DeleteVehicle(Guid id)
        {
            var vehicle = await isVehicleExisted(id);
            if (vehicle == null) throw new Exception("Vehicle does not exists.");
            _context.Vehicles.Remove(vehicle);
            return "Vehicle has deleted successfully";
        }

        private async Task<Vehicle?> isVehicleExisted(Guid id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null) return vehicle;
            return null;
        }
    }
}