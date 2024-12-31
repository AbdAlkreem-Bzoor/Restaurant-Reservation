using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Models.Order
{
    public class OrderCreationDto
    {
        public int ReservationId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
