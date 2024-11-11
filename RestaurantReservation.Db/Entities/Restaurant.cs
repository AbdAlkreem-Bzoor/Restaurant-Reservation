namespace RestaurantReservation.Db.Entities
{
    public class Restaurant
    {
        public int RestaurantId { get; set; } // PK
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public TimeSpan? OpeningHours { get; set; }
        public ICollection<Table> Tables { get; set; } = [];
        public ICollection<Reservation> Reservations { get; set; } = [];
        public ICollection<Employee> Employees { get; set; } = [];
        public ICollection<MenuItem> MenuItems { get; set; } = [];
        public override string ToString()
        {
            return $"[{RestaurantId}] {Name}";
        }
    }
}
