using AutoMapper;
using RestaurantReservation.API.Models.Restaurant;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantCreationDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantResponseDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantUpdateDto>().ReverseMap();
        }
    }
}
