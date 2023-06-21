using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> GetUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("User does not exists");

            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task UpdateUserById(Guid id, UserProfileDto userUpdate)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("User does not exists");

            _mapper.Map(userUpdate, user);
        }

    }
}