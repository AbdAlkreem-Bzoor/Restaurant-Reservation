using AutoMapper;
using RestaurantReservation.API.Models.Order;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderCreationDto>().ReverseMap();
            CreateMap<Order, OrderResponseDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();
        }
    }
}
