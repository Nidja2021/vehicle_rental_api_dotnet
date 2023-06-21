using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:SecretKey").Value!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}