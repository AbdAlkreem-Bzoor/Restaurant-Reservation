using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderItemRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public OrderItemRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItem updatedOrderItem)
        {
            var orderItem = await _context.OrderItems.FindAsync(updatedOrderItem.OrderItemId)
                                  ??
                                  throw new KeyNotFoundException("Order item not found");

            orderItem.Quantity = updatedOrderItem.Quantity;

            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId)
                                  ??
                                  throw new KeyNotFoundException("Order item not found");

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
