using Microsoft.AspNetCore.Mvc;

namespace VehicleRental.API.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                return Ok(await _userService.GetUserById(Guid.Parse(userId!)));
            } catch(Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPatch("profile")]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> UpdateUserProfile([FromBody] UserProfileDto userProfile)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _userService.UpdateUserById(Guid.Parse(userId!), userProfile);
                return NoContent();
            } catch(Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}