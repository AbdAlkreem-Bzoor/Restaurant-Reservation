﻿
namespace RestaurantReservation.API.Models.Reservation
{
    public class ReservationResponseDto
    {
        public int ReservationId { get; set; }

        public int? CustomerId { get; set; }

        public int? RestaurantId { get; set; }

        public int? TableId { get; set; }

        public DateOnly ReservationDate { get; set; }

        public int PartySize { get; set; }
    }
}
