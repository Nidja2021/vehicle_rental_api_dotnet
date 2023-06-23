using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Services.ReservationService
{
    public interface IReservationService
    {
        Task<Reservation> AddReservation(string userId, Reservation reservationRequest);
        Task<Reservation> GetReservation(Guid id, User user);
        Task<Reservation> UpdateReservation(Guid id, User user, Reservation reservationRequest);
        Task DeleteReservation(Guid id, User user);
    }
}