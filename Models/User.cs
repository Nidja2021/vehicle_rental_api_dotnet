using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required] public string? Username { get; set; }
        [Required] public string? Email { get; set; }
        [Required] public string? Password { get; set; }
        public string? FullName { get; set; } = "";
    }
}