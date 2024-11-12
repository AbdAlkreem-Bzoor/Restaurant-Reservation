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
    public class OrderRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public OrderRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order updatedOrder)
        {
            var order = await _context.Orders.FindAsync(updatedOrder.OrderId)
                              ??
                              throw new KeyNotFoundException("Order not found");

            order.OrderDate = updatedOrder.OrderDate;
            order.TotalAmount = updatedOrder.TotalAmount;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId)
                              ??
                              throw new KeyNotFoundException("Order not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
        public async Task<List<KeyValuePair<Order, List<MenuItem>>>>
            ListOrdersAndMenuItemsAsync(int reservationId)
        {
            return await _context.OrderItems.AsNoTracking()
                                          .Include(x => x.Order)
                                          .Where(x => x.Order.ReservationId == reservationId)
                                          .Include(x => x.MenuItem)
                                          .GroupBy(x => x.Order)
                                          .Select(x =>
                                           new KeyValuePair<Order, List<MenuItem>>
                                           (
                                               x.Key,
                                               x.Select(y => y.MenuItem).ToList()
                                           ))
                                          .ToListAsync();
        }
        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            return await _context.Orders.AsNoTracking()
                                  .Where(x => x.EmployeeId == employeeId)
                                  .AverageAsync(x => x.TotalAmount);
        }
    }
}
