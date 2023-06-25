using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Transforms
{
    public class UserTransforms : Profile
    {
        public UserTransforms()
        {
            CreateMap<User, UserDto>();

            CreateMap<User, UserProfileDto>();

            CreateMap<UserProfileDto, User>();
        }
    }
}