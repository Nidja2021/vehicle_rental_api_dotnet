using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VehicleRental.API.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles() {
            return Ok(await _vehicleService.GetVehicles());
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddVehicle([FromBody] VehicleDto vehicleRequest) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vehicleId = await _vehicleService.AddVehicle(vehicleRequest);
            return StatusCode(201, vehicleId);
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(Guid id) {
            var vehicle = await _vehicleService.GetVehicle(id);
            if (vehicle == null) return NotFound(new { Message = "Vehicle does not exists" });
            
            return Ok(vehicle);
        }

        [HttpPatch("/{id}")]
        public async Task<ActionResult<VehicleDto>> UpdateVehicle(Guid id, [FromBody] VehicleDto vehicleRequest) {
            var vehicle = await _vehicleService.GetVehicle(id);
            if (vehicle == null) return NotFound(new { Message = "Vehicle does not exists" });
            
            await _vehicleService.UpdateVehicle(id, vehicleRequest);
            return NoContent();
        }

        [HttpDelete("/{id}")]
        public async Task<ActionResult<string>> DeleteVehicle(Guid id) {
            try
            {
                await _vehicleService.DeleteVehicle(id);
                return NoContent();
            } catch(Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}