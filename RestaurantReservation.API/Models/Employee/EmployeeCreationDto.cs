using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Models.Employee
{
    public class EmployeeCreationDto
    {
        public int RestaurantId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Position { get; set; }
    }
}
