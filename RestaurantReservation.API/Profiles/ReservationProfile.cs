using AutoMapper;
using RestaurantReservation.API.Models.Reservation;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationCreationDto>().ReverseMap();
            CreateMap<Reservation, ReservationResponseDto>().ReverseMap();
            CreateMap<Reservation, ReservationUpdateDto>().ReverseMap();
        }
    }
}
