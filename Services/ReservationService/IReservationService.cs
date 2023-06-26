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
        Task<Reservation> GetReservation(Guid id, Guid userId);
        Task<Reservation> UpdateReservation(Guid id, Guid userId, Reservation reservationRequest);
        Task DeleteReservation(Guid id, Guid userId);
    }
}