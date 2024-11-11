using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Entities
{
    public class EmployeeRestaurantDetail
    {
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public int RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? RestaurantAddress { get; set; }
        public string? RestaurantPhoneNumber { get; set; }

        public override string ToString()
        {
            return $"Employee: {Name} (ID: {EmployeeId}), Position: {Position}\n" +
                   $"Restaurant: {RestaurantName} (ID: {RestaurantId}), Address: {RestaurantAddress}, " +
                   $"Phone: {RestaurantPhoneNumber}";
        }
    }

}
