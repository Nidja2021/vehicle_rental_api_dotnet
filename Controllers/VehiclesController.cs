using Microsoft.AspNetCore.Mvc;

namespace VehicleRental.API.Controllers
{
    [ApiController]
    [Route("api/vehicles/")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles() {
            return Ok(await _vehicleService.GetVehicles());
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Vehicle>> AddVehicle([FromBody] Vehicle vehicleRequest)
        {
            var companyId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (companyId == null) return Forbid();

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return StatusCode(201, await _vehicleService.AddVehicle(Guid.Parse(companyId), vehicleRequest));
            }
            catch (VehicleExistsException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(Guid id) {
            var vehicle = await _vehicleService.GetVehicle(id);
            if (vehicle == null) return NotFound(new { Message = "Vehicle does not exists" });
            
            return Ok(vehicle);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<VehicleDto>> UpdateVehicle(Guid id, [FromBody] VehicleDto vehicleRequest) {
            var vehicle = await _vehicleService.GetVehicle(id);
            if (vehicle == null) return NotFound(new { Message = "Vehicle does not exists" });
            
            await _vehicleService.UpdateVehicle(id, vehicleRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
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

       [HttpGet]
       public async Task<ActionResult<List<VehicleDto>>> GetVehiclesBySearch([FromQuery] string search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10) 
       {
            return Ok(await _vehicleService.GetProductsBySearch(search, page, pageSize));
       }

    }
}