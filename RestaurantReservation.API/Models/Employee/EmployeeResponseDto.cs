namespace RestaurantReservation.API.Models.Employee
{
    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }
        public int? RestaurantId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Position { get; set; } = string.Empty;
    }
}
