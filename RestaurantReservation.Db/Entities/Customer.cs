using RestaurantReservation.Db.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; } // PK
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = [];
        public override string ToString()
        {
            return $"[{CustomerId}] {FirstName} {LastName}";
        }
    }    
}

