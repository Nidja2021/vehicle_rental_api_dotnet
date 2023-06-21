using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Models
{
    public record TokenDto(
        string AccessToken
    );
}