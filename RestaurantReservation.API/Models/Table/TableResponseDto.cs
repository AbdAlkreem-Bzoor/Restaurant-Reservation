namespace RestaurantReservation.API.Models.Table
{
    public class TableResponseDto
    {
        public int TableId { get; set; }
        public int? RestaurantId { get; set; }
        public int Capacity { get; set; }
    }
}
