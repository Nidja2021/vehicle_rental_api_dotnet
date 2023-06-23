using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Services.ReservationService
{
    

    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReservationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Reservation> AddReservation(string userId, Reservation reservationRequest)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => 
                r.VehicleId == reservationRequest.VehicleId && r.UserId == Guid.Parse(userId));

            if (reservation != null) 
                throw new Exception("Reservation already exists");

            var vehicle = await _context.Vehicles.FindAsync(reservationRequest.VehicleId);
            if (vehicle == null) 
                throw new Exception("Vehicle with that id does not exist");

            reservation = new Reservation
            {
                UserId = Guid.Parse(userId),
                VehicleId = vehicle.Id,
                Active = true
            };

            
            
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation> GetReservation(Guid id, User user)
        {
            var reservation = await CheckReservation(id);

            ValidateUserReservation(user, reservation);

            return reservation;
        }

        public async Task<Reservation> UpdateReservation(Guid id, User user, Reservation reservationRequest)
        {
            var reservation = await CheckReservation(id);

            ValidateUserReservation(user, reservation);

            return reservation;
        }

        public async Task DeleteReservation(Guid id, User user)
        {
            var reservation = await CheckReservation(id);

            ValidateUserReservation(user, reservation);

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        private async Task<Reservation> CheckReservation(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) throw new Exception("Reservation does not exists.");
            return null ?? reservation;
        }

        private void ValidateUserReservation(User user, Reservation reservation)
        {
            if (user.Id != reservation?.User?.Id) 
                throw new Exception("You are not authorize for this action");
            return;
        }
    }
}