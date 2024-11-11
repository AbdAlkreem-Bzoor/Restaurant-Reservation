using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Entities
{
    public class ReservationCustomerRestaurantDetail
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int TableId { get; set; }
        public byte PartySize { get; set; }
        public int RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? RestaurantAddress { get; set; }
        public string? RestaurantPhoneNumber { get; set; }

        public override string ToString()
        {
            return $"Customer: {Name} (ID: {CustomerId}), Email: {Email}, Phone: {CustomerPhoneNumber}\n" +
                   $"Reservation: ID {ReservationId}, Date: {ReservationDate}, Table: {TableId}, Party Size: {PartySize}\n" +
                   $"Restaurant: {RestaurantName} (ID: {RestaurantId}), Address: {RestaurantAddress}, " +
                   $"Phone: {RestaurantPhoneNumber}";
        }
    }

}
