using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class MenuItemRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public MenuItemRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task CreateMenuItemAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuItemAsync(MenuItem updatedMenuItem)
        {
            var menuItem = await _context.MenuItems.FindAsync(updatedMenuItem.ItemId)
                                 ??
                                 throw new KeyNotFoundException("Menu item not found");

            menuItem.Name = updatedMenuItem.Name;
            menuItem.Description = updatedMenuItem.Description;
            menuItem.Price = updatedMenuItem.Price;

            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(int itemId)
        {
            var menuItem = await _context.MenuItems.FindAsync(itemId)
                                 ??
                                 throw new KeyNotFoundException("Menu item not found");

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }
        public async Task<List<MenuItem>> ListOrderedMenuItemsAsync(int reservationId)
        {
            return await _context.OrderItems.AsNoTracking()
                                      .Include(x => x.Order)
                                      .Where(x => x.Order.ReservationId == reservationId)
                                      .Include(x => x.MenuItem)
                                      .Select(x => x.MenuItem)
                                      .ToListAsync();
        }
    }
}
