namespace RestaurantReservation.API.Models.Restaurant
{
    public class RestaurantResponseDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public TimeSpan? OpeningHours { get; set; }
    }
}
