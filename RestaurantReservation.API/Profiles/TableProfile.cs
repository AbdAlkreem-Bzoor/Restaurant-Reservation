using AutoMapper;
using RestaurantReservation.API.Models.Table;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<Table, TableCreationDto>().ReverseMap();
            CreateMap<Table, TableResponseDto>().ReverseMap();
            CreateMap<Table, TableUpdateDto>().ReverseMap();
        }
    }
}
