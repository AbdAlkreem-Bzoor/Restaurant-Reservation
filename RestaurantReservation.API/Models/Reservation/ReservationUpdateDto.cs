
namespace RestaurantReservation.API.Models.Reservation
{
    public class ReservationUpdateDto
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public byte PartySize { get; set; }
    }
}
