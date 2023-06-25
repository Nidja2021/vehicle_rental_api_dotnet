using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        public async Task<List<ReservationDto>> GetReservations()
        {
            var reservations = await _context.Reservations
                                .Include(r => r.User)
                                .Include(r => r.Vehicle)
                                .ToListAsync();

            var reservationDto = reservations.Select(rm => new ReservationDto(rm.Id, rm.User!, rm.Vehicle!)).ToList();
            return reservationDto;
        }

        public async Task<ReservationDto> AddReservation(Guid userId, Reservation reservationRequest)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.VehicleId == reservationRequest.VehicleId);
            if (reservation != null) throw new Exception("Reservation already exists");

            reservation = new Reservation
            {
                VehicleId = reservationRequest.VehicleId
            };
            
            var user = await _context.Users.FindAsync(userId);
            
            if (user == null) throw new Exception("User does not exists");

            var vehicle = await _context.Vehicles.FindAsync(reservationRequest.VehicleId);
            if (vehicle == null) throw new Exception("Vehicle does not exists");

            reservation.User = user;
            reservation.Vehicle = vehicle;
            
            await _context.Reservations.AddAsync(reservation);
            // user?.Reservations?.Add(reservation);
            // await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ReservationDto(reservation.Id, reservation.User!, reservation.Vehicle!);
        }

        

        public async Task<ReservationDto> GetReservation(Guid id, Guid userId)
        {
            var reservation = await _context.Reservations
                                .Include(r => r.User)
                                    .ThenInclude(u => u.Reservations)
                                .Include(r => r.Vehicle)
                                .FirstOrDefaultAsync(r => r.Id == id);
                                

            return new ReservationDto(reservation.Id, reservation.User!, reservation.Vehicle!);
        }

        public async Task<Reservation> UpdateReservation(Guid id, User user, Reservation reservationRequest)
        {
            var reservation = await CheckReservation(id);

            ValidateUserReservation(user, reservation);

            return reservation;
        }

        public async Task DeleteReservation(Guid id, Guid userId)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            // var reservation = await _context.Reservations.Where(r => r.UserId == userId).FirstOrDefaultAsync(r => r.Id == id);

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        private async Task<Reservation> CheckReservation(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) throw new Exception("Reservation does not exists.");
            return reservation;
        }

        private void ValidateUserReservation(User user, Reservation reservation)
        {
            // if (user.Id != reservation?.User?.Id) 
            //     throw new Exception("You are not authorize for this action");
            return;
        }
    }
}