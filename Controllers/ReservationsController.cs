using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VehicleRental.API.Controllers
{
    [ApiController]
    [Route("api/reservations/")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly DataContext _context;

        public ReservationsController(IReservationService reservationService, DataContext context)
        {
            _reservationService = reservationService;
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Reservation>> AddReservation(Reservation reservationRequest)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("Token is expired");
 
            try
            {
                var reservation = await _reservationService.AddReservation(userId, reservationRequest);
                return StatusCode(201, reservation);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Reservation>> GetReserveration(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("Token is expired");

            try
            {
                var user = await GetCurrentUser(userId);
                var reservation = await _reservationService.GetReservation(id, user);
                return Ok(reservation);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Reservation>> DeleteReserveration(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("Token is expired");

            try
            {
                var user = await GetCurrentUser(userId);
                await _reservationService.DeleteReservation(id, user);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        private async Task<User> GetCurrentUser(string userId)
        {
            var user = await _context.Users.FindAsync(Guid.Parse(userId));
            if (user == null) throw new Exception("User does not exists");

            return user;
        }
    }
}