namespace RestaurantReservation.Db.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; } // PK
        public int RestaurantId { get; set; } // FK (Restaurant)
        public Restaurant Restaurant { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Position { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
        public override string ToString()
        {
            return $"[{EmployeeId}] {FirstName} {LastName}";
        }
    }
}
