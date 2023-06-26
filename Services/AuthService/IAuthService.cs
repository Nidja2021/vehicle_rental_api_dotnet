using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Services.AuthService
{
    public interface IAuthService
    {
        Task<UserDto> Register(User userRegister, string role = "User");
        Task<TokenDto> Login(UserLoginDto userLogin);
    }
}