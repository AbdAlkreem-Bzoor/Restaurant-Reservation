using AutoMapper;
using RestaurantReservation.API.Models.User;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginRequestDto>().ReverseMap();
            CreateMap<User, RegisterRequestDto>().ReverseMap();
            CreateMap<User, UserWithoutPasswordDto>().ReverseMap();
        }
    }
}
