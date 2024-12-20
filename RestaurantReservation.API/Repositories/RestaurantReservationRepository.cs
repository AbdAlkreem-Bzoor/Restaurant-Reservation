using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RestaurantReservation.API.Repositories
{
    public class RestaurantReservationRepository : IRestaurantReservationRepository
    {
        private readonly ILogger<RestaurantReservationRepository> _logger;
        private readonly RestaurantReservationDbContext _database;
        public RestaurantReservationRepository(RestaurantReservationDbContext database,
            ILogger<RestaurantReservationRepository> logger)
        {
            _database = database;
            _logger = logger;
        }
        #region Customer
        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            if (await _database.Customers.AnyAsync(x => x.CustomerId == customer.CustomerId))
            {
                _logger.LogInformation($"Customer already exist {customer}");
                return false;
            }
            await _database.Customers.AddAsync(customer);
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _database.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            if (customer is null)
            {
                _logger.LogInformation($"Customer doesn't exist with id: [{id}]");
                return false;
            }
            _database.Customers.Remove(customer);
            return true;
        }

        public async Task<Customer?> GetCustomerAsync(int id)
        {
            return await _database.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _database.Customers.ToListAsync();
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var databaseCustomer = await _database.Customers.FirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId);
            if (databaseCustomer is null)
            {
                _logger.LogInformation($"Customer doesn't exist with id: [\n\r{customer}\r\n]");
                return false;
            }

            databaseCustomer.FirstName = customer.FirstName;
            databaseCustomer.LastName = customer.LastName;
            databaseCustomer.Email = customer.Email;
            databaseCustomer.PhoneNumber = customer.PhoneNumber;

            return true;
        }
#endregion Customer
        #region Employee
        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            if (await _database.Employees.AnyAsync(x => x.EmployeeId == employee.EmployeeId))
            {
                _logger.LogInformation($"Employee already exist {employee}");
                return false;
            }
            await _database.Employees.AddAsync(employee);
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _database.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (employee is null)
            {
                _logger.LogInformation($"Employee doesn't exist with id: [{id}]");
                return false;
            }
            _database.Employees.Remove(employee);
            return true;
        }

        public async Task<Employee?> GetEmployeeAsync(int id)
        {
            return await _database.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _database.Employees.ToListAsync();
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var databaseEmployee = await _database.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);
            if (databaseEmployee is null)
            {
                _logger.LogInformation($"Employee doesn't exist with id: [\n\r{employee}\r\n]");
                return false;
            }

            databaseEmployee.FirstName = employee.FirstName;
            databaseEmployee.LastName = employee.LastName;
            databaseEmployee.Position = employee.Position;

            return true;
        }
        public async Task<IEnumerable<Employee>> ListManagersAsync()
        {
            return await _database.Employees.AsNoTracking()
                                           .Where(e => e.Position == "Manager")
                                           .ToListAsync();
        }
        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            return await _database.Employees.AsNoTracking()
                                            .Where(e => e.EmployeeId == employeeId)
                                            .Include(e => e.Orders)
                                            .SelectMany(e => e.Orders)
                                            .AverageAsync(o => o.TotalAmount);
        }
        #endregion Employee
        #region MenuItem
        public async Task<bool> AddMenuItemAsync(MenuItem menuItem)
        {
            if (await _database.MenuItems.AnyAsync(x => x.ItemId == menuItem.ItemId))
            {
                _logger.LogInformation($"Menu Item already exist {menuItem}");
                return false;
            }
            await _database.MenuItems.AddAsync(menuItem);
            return true;
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            var menuItem = await _database.MenuItems.FirstOrDefaultAsync(x => x.ItemId == id);
            if (menuItem is null)
            {
                _logger.LogInformation($"Menu Item doesn't exist with id: [{id}]");
                return false;
            }
            _database.MenuItems.Remove(menuItem);
            return true;
        }

        public async Task<MenuItem?> GetMenuItemAsync(int id)
        {
            return await _database.MenuItems.FirstOrDefaultAsync(x => x.ItemId == id);
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync()
        {
            return await _database.MenuItems.ToListAsync();
        }

        public async Task<bool> UpdateMenuItemAsync(MenuItem menuItem)
        {
            var databaseMenuItem = await _database.MenuItems.FirstOrDefaultAsync(x => x.ItemId == menuItem.ItemId);
            if (databaseMenuItem is null)
            {
                _logger.LogInformation($"Menu Item doesn't exist with id: [\n\r{menuItem}\r\n]");
                return false;
            }

            databaseMenuItem.Price = menuItem.Price;
            databaseMenuItem.Description = menuItem.Description;

            return true;
        }
#endregion MenuItem
        #region Order
        public async Task<bool> AddOrderAsync(Order order)
        {
            if (await _database.Orders.AnyAsync(x => x.OrderId == order.OrderId))
            {
                _logger.LogInformation($"Order already exist {order}");
                return false;
            }
            await _database.Orders.AddAsync(order);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _database.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (order is null)
            {
                _logger.LogInformation($"Order doesn't exist with id: [{id}]");
                return false;
            }
            _database.Orders.Remove(order);
            return true;
        }

        public async Task<Order?> GetOrderAsync(int id)
        {
            return await _database.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _database.Orders.ToListAsync();
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var databaseOrder = await _database.Orders.FirstOrDefaultAsync(x => x.OrderId == order.OrderId);
            if (databaseOrder is null)
            {
                _logger.LogInformation($"Order doesn't exist with id: [\n\r{order}\r\n]");
                return false;
            }

            databaseOrder.OrderDate = order.OrderDate;
            databaseOrder.TotalAmount = order.TotalAmount;

            return true;
        }
#endregion Order
        #region OrderItem
        public async Task<bool> AddOrderItemAsync(OrderItem orderItem)
        {
            if (await _database.OrderItems.AnyAsync(x => x.OrderItemId == orderItem.OrderItemId))
            {
                _logger.LogInformation($"Order-Item already exist {orderItem}");
                return false;
            }
            await _database.OrderItems.AddAsync(orderItem);
            return true;
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await _database.OrderItems.FirstOrDefaultAsync(x => x.OrderItemId == id);
            if (orderItem is null)
            {
                _logger.LogInformation($"Order-Item doesn't exist with id: [{id}]");
                return false;
            }
            _database.OrderItems.Remove(orderItem);
            return true;
        }

        public async Task<OrderItem?> GetOrderItemAsync(int id)
        {
            return await _database.OrderItems.FirstOrDefaultAsync(x => x.OrderItemId == id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync()
        {
            return await _database.OrderItems.ToListAsync();
        }

        public async Task<bool> UpdateOrderItemAsync(OrderItem orderItem)
        {
            var databaseOrderItem = await _database.OrderItems.FirstOrDefaultAsync(x => x.OrderItemId == orderItem.OrderItemId);
            if (databaseOrderItem is null)
            {
                _logger.LogInformation($"Order-Item doesn't exist with id: [\n\r{orderItem}\r\n]");
                return false;
            }

            databaseOrderItem.Quantity = orderItem.Quantity;

            return true;
        }
#endregion OrderItem
        #region Reservation
        public async Task<bool> AddReservationAsync(Reservation reservation)
        {
            if (await _database.Reservations.AnyAsync(x => x.ReservationId == reservation.ReservationId))
            {
                _logger.LogInformation($"Reservation already exist {reservation}");
                return false;
            }
            await _database.Reservations.AddAsync(reservation);
            return true;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _database.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            if (reservation is null)
            {
                _logger.LogInformation($"Reservation doesn't exist with id: [{id}]");
                return false;
            }
            _database.Reservations.Remove(reservation);
            return true;
        }

        public async Task<Reservation?> GetReservationAsync(int id)
        {
            return await _database.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return await _database.Reservations.ToListAsync();
        }

        public async Task<bool> UpdateReservationAsync(Reservation reservation)
        {
            var databaseReservation = await _database.Reservations.FirstOrDefaultAsync(x => x.ReservationId == reservation.ReservationId);
            if (databaseReservation is null)
            {
                _logger.LogInformation($"Reservation doesn't exist with id: [\n\r{reservation}\r\n]");
                return false;
            }

            databaseReservation.ReservationDate = reservation.ReservationDate;
            databaseReservation.PartySize = reservation.PartySize;

            return true;
        }
        public async Task<IEnumerable<Reservation>> GetReservationsForCustomerAsync(int customerId)
        {
            return await _database.Customers.AsNoTracking()
                                    .Include(c => c.Reservations)
                                    .Where(c => c.CustomerId == customerId)
                                    .SelectMany(c => c.Reservations)
                                    .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersForReservationAsync(int reservationId)
        {
            return await _database.Reservations.AsNoTracking()
                                         .Where(r => r.ReservationId == reservationId)
                                         .Include(r => r.Orders)
                                         .SelectMany(r => r.Orders)
                                         .ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetMenuItemsForReservationAsync(int reservationId)
        {
            return await _database.OrderItems.AsNoTracking()
                                       .Include(oi => oi.MenuItem)
                                       .Include(oi => oi.Order)
                                       .ThenInclude(o => o.Reservation)
                                       .Where(oi => oi.Order.Reservation.ReservationId == reservationId)
                                       .Select(oi => oi.MenuItem)
                                       .ToListAsync();
        }
        public async Task<IEnumerable<KeyValuePair<Order, List<MenuItem>>>> GetOrdersAndMenuItemForReservationAsync(int reservationId)
        {
            return await _database.OrderItems.AsNoTracking()
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
        #endregion Reservation
        #region Restaurant
        public async Task<bool> AddRestaurantAsync(Restaurant table)
        {
            if (await _database.Restaurants.AnyAsync(x => x.RestaurantId == table.RestaurantId))
            {
                _logger.LogInformation($"Restaurant already exist {table}");
                return false;
            }
            await _database.Restaurants.AddAsync(table);
            return true;
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _database.Restaurants.FirstOrDefaultAsync(x => x.RestaurantId == id);
            if (restaurant is null)
            {
                _logger.LogInformation($"Restaurant doesn't exist with id: [{id}]");
                return false;
            }
            _database.Restaurants.Remove(restaurant);
            return true;
        }

        public async Task<Restaurant?> GetRestaurantAsync(int id)
        {
            return await _database.Restaurants.FirstOrDefaultAsync(x => x.RestaurantId == id);
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
        {
            return await _database.Restaurants.ToListAsync();
        }

        public async Task<bool> UpdateRestaurantAsync(Restaurant restaurant)
        {
            var databaseRestaurant = await _database.Restaurants.FirstOrDefaultAsync(x => x.RestaurantId == restaurant.RestaurantId);
            if (databaseRestaurant is null)
            {
                _logger.LogInformation($"Restaurant doesn't exist with id: [\n\r{restaurant}\r\n]");
                return false;
            }

            databaseRestaurant.Address = restaurant.Address;
            databaseRestaurant.PhoneNumber = restaurant.PhoneNumber;
            databaseRestaurant.OpeningHours = restaurant.OpeningHours;
            databaseRestaurant.Name = restaurant.Name;

            return true;
        }
#endregion Restaurant
        #region Table
        public async Task<bool> AddTableAsync(Table table)
        {
            if (await _database.Tables.AnyAsync(x => x.TableId == table.TableId))
            {
                _logger.LogInformation($"Table already exist {table}");
                return false;
            }
            await _database.Tables.AddAsync(table);
            return true;
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _database.Tables.FirstOrDefaultAsync(x => x.TableId == id);
            if (table is null)
            {
                _logger.LogInformation($"Table doesn't exist with id: [{id}]");
                return false;
            }
            _database.Tables.Remove(table);
            return true;
        }

        public async Task<Table?> GetTableAsync(int id)
        {
            return await _database.Tables.FirstOrDefaultAsync(x => x.TableId == id);
        }

        public async Task<IEnumerable<Table>> GetTablesAsync()
        {
            return await _database.Tables.ToListAsync();
        }

        public async Task<bool> UpdateTableAsync(Table table)
        {
            var databaseTable = await _database.Tables.FirstOrDefaultAsync(x => x.TableId == table.TableId);
            if (databaseTable is null)
            {
                _logger.LogInformation($"Table doesn't exist with id: [\n\r{table}\r\n]");
                return false;
            }

            databaseTable.Capacity = table.Capacity;

            return true;
        }
        #endregion Table
        #region User
        public async Task<bool> AddUserAsync(User user)
        {
            if (await _database.Users.AnyAsync(x => x.Id == user.Id))
            {
                _logger.LogInformation($"User already exist {user}");
                return false;
            }
            await _database.Users.AddAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _database.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                _logger.LogInformation($"User doesn't exist with id: [{id}]");
                return false;
            }
            _database.Users.Remove(user);
            return true;
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _database.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _database.Users.ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var databaseUser = await _database.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (databaseUser is null)
            {
                _logger.LogInformation($"User doesn't exist with id: [\n\r{user}\r\n]");
                return false;
            }

            databaseUser.UserName = user.UserName;
            databaseUser.Password = user.Password;
            databaseUser.Role = user.Role;
            databaseUser.Email = user.Email;

            return true;
        }
        #endregion
        public async Task<bool> SaveChangesAsync()
        {
            return (await _database.SaveChangesAsync()) > 0;
        }
    }
    public class OrdersComparer : IEqualityComparer<Order>
    {
        public bool Equals(Order? x, Order? y) => x?.OrderId == y?.OrderId;
        public int GetHashCode([DisallowNull] Order obj) => HashCode.Combine(obj.OrderId);
    }
}
