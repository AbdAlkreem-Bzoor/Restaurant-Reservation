using AutoMapper;
using RestaurantReservation.API.Models.Customer;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerCreationDto>().ReverseMap();
            CreateMap<Customer, CustomerResponseDto>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDto>().ReverseMap();
        }
    }
}
