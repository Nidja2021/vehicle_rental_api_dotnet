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

        [HttpGet]
        public async Task<ActionResult<List<ReservationDto>>> GetReservations()
        {
            return Ok(await _reservationService.GetReservations());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReservationDto>> AddReservation(Reservation reservationRequest)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();
 
            try
            {
                var reservation = await _reservationService.AddReservation(Guid.Parse(userId), reservationRequest);
                // return StatusCode(201, reservation);
                return CreatedAtAction(nameof(GetReserveration), new { id = reservation.Id}, reservation);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ReservationDto>> GetReserveration(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            try
            {
                var reservation = await _reservationService.GetReservation(id, Guid.Parse(userId));
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
            if (userId == null) return Unauthorized();

            try
            {
                await _reservationService.DeleteReservation(id, Guid.Parse(userId));
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}