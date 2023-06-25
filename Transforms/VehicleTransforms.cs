using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace VehicleRental.API.Transforms
{
    public class VehicleTransforms : Profile
    {
        public VehicleTransforms()
        {
            CreateMap<VehicleDto, Vehicle>();

            CreateMap<Vehicle, VehicleDto>();

        }
    }
}