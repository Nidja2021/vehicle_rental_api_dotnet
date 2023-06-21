using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Services.UserService
{
    public interface IUserService
    {
        Task<UserProfileDto> GetUserById(Guid id);
        Task UpdateUserById(Guid id, UserProfileDto userUpdate);
    }
}