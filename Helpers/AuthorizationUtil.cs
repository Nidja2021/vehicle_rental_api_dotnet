using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Helpers
{
    public class AuthorizationUtil
    {
        private readonly DataContext _context;

        public AuthorizationUtil(DataContext context)
        {
            _context = context;
        }
        
    }
}