namespace RestaurantReservation.Db.Entities
{
    public class Table
    {
        public int TableId { get; set; } // PK
        public int RestaurantId { get; set; } // FK (Restaurant)
        public Restaurant Restaurant { get; set; } = null!;
        public int Capacity { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}
