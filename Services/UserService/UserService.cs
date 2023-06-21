using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Services.UserService
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(
            DataContext context, 
            IMapper mapper,
            IConfiguration configuration
        ) {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

    
    }
}