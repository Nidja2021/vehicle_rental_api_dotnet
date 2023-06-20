using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? FuelType { get; set; }
        public decimal RentalRate { get; set; }
    }
}