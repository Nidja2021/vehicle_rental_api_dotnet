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
            if (reservation != null) throw new ReservationExistsException();

            reservation = new Reservation
            {
                VehicleId = reservationRequest.VehicleId
            };
            
            var user = await _context.Users.FindAsync(userId);
            
            if (user == null) throw new UserNotFoundException();

            var vehicle = await _context.Vehicles.FindAsync(reservationRequest.VehicleId);
            if (vehicle == null) throw new VehicleNotFoundException();

            reservation.User = user;
            reservation.Vehicle = vehicle;
            
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return new ReservationDto(reservation.Id, reservation.User!, reservation.Vehicle!);
        }

        

        public async Task<Reservation> GetReservation(Guid id, Guid userId)
        {
            var reservation = await _context.Reservations
                                .Include(r => r.User)
                                    .ThenInclude(u => u.Reservations)
                                .Include(r => r.Vehicle)
                                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) throw new ReservationNotFoundException();
                                
            return reservation!;
        }

        public async Task<Reservation> UpdateReservation(Guid id, Guid userId, Reservation reservationRequest)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null) throw new ReservationNotFoundException();

            reservation.StartReservation = reservationRequest.StartReservation;
            reservation.EndReservation = reservationRequest.EndReservation;

            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task DeleteReservation(Guid id, Guid userId)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) throw new ReservationNotFoundException();

            _context.Reservations.Remove(reservation!);
            await _context.SaveChangesAsync();
        }
    }
}