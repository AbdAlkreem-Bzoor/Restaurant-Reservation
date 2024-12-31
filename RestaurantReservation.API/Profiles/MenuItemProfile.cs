using AutoMapper;
using RestaurantReservation.API.Models.MenuItem;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemCreationDto>().ReverseMap();
            CreateMap<MenuItem, MenuItemResponseDto>().ReverseMap();
            CreateMap<MenuItem, MenuItemUpdateDto>().ReverseMap();
        }
    }
}
