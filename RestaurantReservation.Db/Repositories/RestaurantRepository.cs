using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class RestaurantRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public RestaurantRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task CreateRestaurantAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRestaurantAsync(Restaurant updatedRestaurant)
        {
            var restaurant = await _context.Restaurants.FindAsync(updatedRestaurant.RestaurantId)
                                   ??
                                   throw new KeyNotFoundException("Restaurant not found");

            restaurant.Name = updatedRestaurant.Name;
            restaurant.Address = updatedRestaurant.Address;
            restaurant.PhoneNumber = updatedRestaurant.PhoneNumber;
            restaurant.OpeningHours = updatedRestaurant.OpeningHours;

            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantId)
                                   ??
                                   throw new KeyNotFoundException("Restaurant not found");

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
