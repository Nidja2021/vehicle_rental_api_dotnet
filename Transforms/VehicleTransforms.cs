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
            CreateMap<VehicleDto, Vehicle>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => src.Brand)
                )
                .ForMember(
                    dest => dest.Model,
                    opt => opt.MapFrom(src => src.Model)
                )
                .ForMember(
                    dest => dest.Year,
                    opt => opt.MapFrom(src => src.Year)
                )
                .ForMember(
                    dest => dest.FuelType,
                    opt => opt.MapFrom(src => src.FuelType)
                )
                .ForMember(
                    dest => dest.RentalRate,
                    opt => opt.MapFrom(src => src.RentalRate)
                );

            CreateMap<Vehicle, VehicleDto>()
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => src.Brand)
                )
                .ForMember(
                    dest => dest.Model,
                    opt => opt.MapFrom(src => src.Model)
                )
                .ForMember(
                    dest => dest.Year,
                    opt => opt.MapFrom(src => src.Year)
                )
                .ForMember(
                    dest => dest.FuelType,
                    opt => opt.MapFrom(src => src.FuelType)
                )
                .ForMember(
                    dest => dest.RentalRate,
                    opt => opt.MapFrom(src => src.RentalRate)
                );

            CreateMap<VehicleDto, Vehicle>()
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => src.Brand)
                )
                .ForMember(
                    dest => dest.Model,
                    opt => opt.MapFrom(src => src.Model)
                )
                .ForMember(
                    dest => dest.Year,
                    opt => opt.MapFrom(src => src.Year)
                )
                .ForMember(
                    dest => dest.FuelType,
                    opt => opt.MapFrom(src => src.FuelType)
                )
                .ForMember(
                    dest => dest.RentalRate,
                    opt => opt.MapFrom(src => src.RentalRate)
                );

        }
    }
}