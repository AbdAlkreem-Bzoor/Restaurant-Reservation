namespace RestaurantReservation.API.Models.Order
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int? ReservationId { get; set; }
        public int? EmployeeId { get; set; }
        public DateOnly OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
