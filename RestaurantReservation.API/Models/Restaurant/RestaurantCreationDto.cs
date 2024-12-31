namespace RestaurantReservation.API.Models.Restaurant
{
    public class RestaurantCreationDto
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public TimeSpan? OpeningHours { get; set; }
    }
}
