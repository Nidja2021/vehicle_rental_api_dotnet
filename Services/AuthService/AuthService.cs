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

        public async Task<UserDto> Register(User userRegister)
        {
            var isUserExists = await _context.Users.FirstOrDefaultAsync(user => user.Email == userRegister.Email || user.Username == userRegister.Username);
            if (isUserExists != null) throw new Exception("User already exists");

            userRegister.Password = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);

            await _context.Users.AddAsync(userRegister);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(userRegister);
            return userDto;
        }

        public async Task<TokenDto> Login(UserLoginDto userLogin)
        {
            var isUserExists = await _context.Users.FirstOrDefaultAsync(user => user.Email == userLogin.Email);
            if (isUserExists == null) throw new Exception("User does not exists");
            
            var checkPassword = BCrypt.Net.BCrypt.Verify(userLogin.Password, isUserExists.Password);
            if (!checkPassword) throw new Exception("Invalid credentials");

            var token = GenerateToken(isUserExists);
            var tokenDto = new TokenDto(token);
            return tokenDto;
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:SecretKey").Value!
            ));

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature
            );

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                issuer: _configuration.GetSection("JWT:ValidIssuer").Value!,
                audience: _configuration.GetSection("JWT:ValidAudience").Value!,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}