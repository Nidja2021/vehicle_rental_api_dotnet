using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VehicleRental.API.Controllers
{
    [ApiController]
    [Route("api/auth/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("admin/register")]
        public async Task<ActionResult<UserDto>> AdminRegister(User userRegister)
        {
            try
            {
                return StatusCode(201, await _authService.Register(userRegister, "Admin"));
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPost("admin/login")]
        public async Task<ActionResult<UserDto>> AdminLogin(UserLoginDto userLogin)
        {
            try
            {
                return StatusCode(201, await _authService.Login(userLogin));
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> UserRegister(User userRegister)
        {
            try
            {
                return StatusCode(201, await _authService.Register(userRegister));
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> UserLogin(UserLoginDto userLogin)
        {
            try
            {
                return Ok(await _authService.Login(userLogin));
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}