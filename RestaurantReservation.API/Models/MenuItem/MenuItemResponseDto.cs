namespace RestaurantReservation.API.Models.MenuItem
{
    public class MenuItemResponseDto
    {
        public int ItemId { get; set; }

        public int? RestaurantId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty!;

        public decimal Price { get; set; }
    }
}
