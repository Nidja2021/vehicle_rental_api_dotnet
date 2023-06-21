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
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(src => src.Username)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                )
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FullName)
                );

            CreateMap<User, UserProfileDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(src => src.Username)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                )
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FullName)
                );

            CreateMap<UserProfileDto, User>()
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(src => src.Username)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                )
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FullName)
                );
        }
    }
}