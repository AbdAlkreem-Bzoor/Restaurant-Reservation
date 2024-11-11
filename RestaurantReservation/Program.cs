using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation
{
    public class Program
    {
        private RestaurantReservationDbContext _context = null!;
        public static void Main(string[] args)
        {
            
        }
        public async Task<List<Employee>> ListManagersAsync()
        {
            return await _context.Employees.AsNoTracking()
                                           .Where(e => e.Position == "Manager")
                                           .ToListAsync();
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
        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            return await _context.Customers.AsNoTracking()
                                    .Include(c => c.Reservations)
                                    .Where(c => c.CustomerId == customerId)
                                    .SelectMany(c => c.Reservations)
                                    .ToListAsync();
        }
        public async Task CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer updatedCustomer)
        {
            var customer = await _context.Customers.FindAsync(updatedCustomer.CustomerId)
                                 ??
                                 throw new KeyNotFoundException("Customer not found");
            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Email = updatedCustomer.Email;
            customer.PhoneNumber = updatedCustomer.PhoneNumber;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId)
                                 ??
                                 throw new KeyNotFoundException("Customer not found");
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee updatedEmployee)
        {
            var employee = await _context.Employees.FindAsync(updatedEmployee.EmployeeId)
                                 ??
                                 throw new KeyNotFoundException("Employee not found");

            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Position = updatedEmployee.Position;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId)
                                 ??
                                 throw new KeyNotFoundException("Employee not found");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
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
        public async Task CreateReservationAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(Reservation updatedReservation)
        {
            var reservation = await _context.Reservations.FindAsync(updatedReservation.ReservationId)
                                    ??
                                    throw new KeyNotFoundException("Reservation not found");

            reservation.ReservationDate = updatedReservation.ReservationDate;
            reservation.PartySize = updatedReservation.PartySize;

            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId)
                                    ??
                                    throw new KeyNotFoundException("Reservation not found");

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
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
        public async Task CreateTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTableAsync(Table updatedTable)
        {
            var table = await _context.Tables.FindAsync(updatedTable.TableId)
                              ??
                              throw new KeyNotFoundException("Table not found");

            table.Capacity = updatedTable.Capacity;

            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId)
                              ??
                              throw new KeyNotFoundException("Table not found");

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }
    }
}
