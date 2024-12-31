using AutoMapper;
using RestaurantReservation.API.Models.Employee;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeCreationDto>().ReverseMap();
            CreateMap<Employee, EmployeeResponseDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
        }
    }
}
