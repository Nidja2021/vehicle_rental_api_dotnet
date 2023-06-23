using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Transforms
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>()
                .ForMember(
                    dest => dest.Vehicle,
                    opt => opt.MapFrom(src => src.Vehicle)
                )
                .ForMember(
                    dest => dest.StartReservation,
                    opt => opt.MapFrom(src => src.StartReservation)
                )
                .ForMember(
                    dest => dest.EndReservation,
                    opt => opt.MapFrom(src => src.EndReservation)
                );
        }
    }
}