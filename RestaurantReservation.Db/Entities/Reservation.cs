namespace RestaurantReservation.Db.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; } // PK
        public int CustomerId { get; set; } // FK (Customer)
        public Customer Customer { get; set; } = null!;
        public int RestaurantId { get; set; } // FK (Restaurant)
        public Restaurant Restaurant { get; set; } = null!;
        public int TableId { get; set; } // FK (Table)
        public Table Table { get; set; } = null!;
        public DateOnly ReservationDate { get; set; }
        public byte PartySize { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
        public override string ToString()
        {
            return $"Reservation ID : {ReservationId}, Reservation Date : {ReservationDate}";
        }
    }
}
