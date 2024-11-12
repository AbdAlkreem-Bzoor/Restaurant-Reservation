using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Entities
{
    public enum ReservationStatusType
    {
        Pending,
        Confirmed,
        NotAttended,
        Completed,
        Cancelled,
        Rescheduled,
        CancelledByRestaurant
    }
    public class ReservationStatus
    {
        public int ReservationStatusId { get; set; } // PK
        public int ReservationId { get; set; } // FK (Unique)
        public ReservationStatusType Status { get; set; }
        public DateOnly StatusDate { get; set; }
        public Reservation Reservation { get; set; } = null!;
    }
}
