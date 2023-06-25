using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Services.ReservationService
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetReservations();
        Task<ReservationDto> AddReservation(Guid userId, Reservation reservationRequest);
        Task<ReservationDto> GetReservation(Guid id, Guid userId);
        Task<Reservation> UpdateReservation(Guid id, User user, Reservation reservationRequest);
        Task DeleteReservation(Guid id, Guid userId);
    }
}