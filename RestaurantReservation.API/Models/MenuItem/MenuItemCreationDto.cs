using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Models.MenuItem
{
    public class MenuItemCreationDto
    {
        public int? RestaurantId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
