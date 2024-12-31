using AutoMapper;
using RestaurantReservation.API.Models.OrderItem;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemCreationDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemResponseDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemUpdateDto>().ReverseMap();
        }
    }
}
