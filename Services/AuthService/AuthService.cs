namespace VehicleRental.API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserDto> Register(User userRegister, string role)
        {
            bool isUserExists = await _context.Users.AnyAsync(c => c.Email == userRegister.Email);
            if (isUserExists) throw new UserExistsException();

            userRegister.Email = userRegister.Email;
            userRegister.Password = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
            userRegister.Reservations = new List<Reservation>();

            if (role == "Admin")
            {
                userRegister.Role = Role.ADMIN;
            }

            await _context.Users.AddAsync(userRegister);
            await _context.SaveChangesAsync();

            return new UserDto(userRegister.Email);
        }

        public async Task<TokenDto> Login(UserLoginDto userLogin)
        {
            var user = await AuthClass.GetEntityByEmailAsync<User>(_context, userLogin.Email!);
            if (user == null) throw new UserNotFoundException();
            
            var checkPassword = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);
            if (!checkPassword) throw new InvalidCredentialsException();

            var token = AuthClass.GenerateToken<User>(user, _configuration);
            var tokenDto = new TokenDto(token);
            return tokenDto;
        }

    }
}