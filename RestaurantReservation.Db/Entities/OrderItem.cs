namespace RestaurantReservation.Db.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; } // PK
        public int OrderId { get; set; } // FK (Order)
        public Order Order { get; set; } = null!;
        public int ItemId { get; set; } // FK (MenuItem)
        public MenuItem MenuItem { get; set; } = null!;
        public int Quantity { get; set; }
        public override string ToString()
        {
            return $"ID is {OrderItemId}, Order ID is {OrderId}, MenuItem ID is {ItemId}";
        }
    }
}
