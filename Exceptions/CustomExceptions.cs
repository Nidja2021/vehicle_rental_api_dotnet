
namespace VehicleRental.API.Exceptions
{

    public class UserExistsException : Exception
    {
        public UserExistsException() : base("User already exists") {}
    }

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found") {}
    }

    public class ReservationExistsException : Exception
    {
        public ReservationExistsException() : base("Reservation already exists") {}
    }
    public class ReservationNotFoundException : Exception
    {
        public ReservationNotFoundException() : base("Reservation not found") {}
    }

    public class VehicleExistsException : Exception
    {
        public VehicleExistsException() : base("Vehicle already exists") {}
    }

    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException() : base("Vehicle not found") {}
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid credentials") {}
    }
}