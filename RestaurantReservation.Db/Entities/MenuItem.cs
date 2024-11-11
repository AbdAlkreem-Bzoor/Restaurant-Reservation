namespace RestaurantReservation.Db.Entities
{
    public class MenuItem
    {
        public int ItemId { get; set; } // PK
        public int RestaurantId { get; set; } // FK (Restaurant)
        public Restaurant Restaurant { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public override string ToString()
        {
            return $"[{ItemId}] , {Description} , {Price}";
        }
    }
}
