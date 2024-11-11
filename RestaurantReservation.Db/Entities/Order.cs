namespace RestaurantReservation.Db.Entities
{
    public class Order
    {
        public int OrderId { get; set; } // PK
        public int ReservationId { get; set; } // FK (Reservation)
        public Reservation Reservation { get; set; } = null!;
        public int EmployeeId { get; set; } // FK (Employee)
        public Employee Employee { get; set; } = null!;
        public DateOnly OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public override string ToString()
        {
            return $"[{OrderId}] {ReservationId}, ordered at {OrderDate}";
        }
    }
}
