using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Data
{
    public class SeedData
    {
        public static Customer[] LoadCustomers()
        {
            return [
                       new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "555-0101", Reservations = new List<Reservation>() },
                       new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "555-0102", Reservations = new List<Reservation>() },
                       new Customer { CustomerId = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", PhoneNumber = "555-0103", Reservations = new List<Reservation>() },
                       new Customer { CustomerId = 4, FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", PhoneNumber = "555-0104", Reservations = new List<Reservation>() },
                       new Customer { CustomerId = 5, FirstName = "Charlie", LastName = "Davis", Email = "charlie.davis@example.com", PhoneNumber = "555-0105", Reservations = new List<Reservation>() }
                   ];
        }
        public static Employee[] LoadEmployees()
        {
            return [
                       new Employee { EmployeeId = 1, RestaurantId = 1, FirstName = "Sophia", LastName = "Lee", Position = "Manager", Orders = new List<Order>() },
                       new Employee { EmployeeId = 2, RestaurantId = 1, FirstName = "James", LastName = "King", Position = "Waiter", Orders = new List<Order>() },
                       new Employee { EmployeeId = 3, RestaurantId = 2, FirstName = "Oliver", LastName = "Scott", Position = "Cook", Orders = new List<Order>() },
                       new Employee { EmployeeId = 4, RestaurantId = 2, FirstName = "Liam", LastName = "Evans", Position = "Waiter", Orders = new List<Order>() },
                       new Employee { EmployeeId = 5, RestaurantId = 3, FirstName = "Mia", LastName = "Taylor", Position = "Waitress", Orders = new List<Order>() }
                   ];
        }
        public static MenuItem[] LoadMenuItems()
        {
            return [
                       new MenuItem { ItemId = 1, RestaurantId = 1, Name = "Cheeseburger", Description = "Beef patty with cheese", Price = 12.99m, OrderItems = new List<OrderItem>() },
                       new MenuItem { ItemId = 2, RestaurantId = 1, Name = "Caesar Salad", Description = "Fresh lettuce with Caesar dressing", Price = 8.99m, OrderItems = new List<OrderItem>() },
                       new MenuItem { ItemId = 3, RestaurantId = 2, Name = "Spaghetti Carbonara", Description = "Spaghetti with creamy carbonara sauce", Price = 14.99m, OrderItems = new List<OrderItem>() },
                       new MenuItem { ItemId = 4, RestaurantId = 2, Name = "Margherita Pizza", Description = "Tomato, mozzarella, and basil pizza", Price = 11.99m, OrderItems = new List<OrderItem>() },
                       new MenuItem { ItemId = 5, RestaurantId = 3, Name = "Salmon Sashimi", Description = "Fresh slices of salmon", Price = 18.99m, OrderItems = new List<OrderItem>() }
                   ];
        }
        public static Order[] LoadOrders()
        {
            return [
                       new Order { OrderId = 1, ReservationId = 1, EmployeeId = 1, OrderDate = new DateOnly(2024, 11, 7), TotalAmount = 20, OrderItems = new List<OrderItem>() },
                       new Order { OrderId = 2, ReservationId = 2, EmployeeId = 2, OrderDate = new DateOnly(2024, 11, 8), TotalAmount = 25, OrderItems = new List<OrderItem>() },
                       new Order { OrderId = 3, ReservationId = 3, EmployeeId = 3, OrderDate = new DateOnly(2024, 11, 9), TotalAmount = 18, OrderItems = new List<OrderItem>() },
                       new Order { OrderId = 4, ReservationId = 4, EmployeeId = 4, OrderDate = new DateOnly(2024, 11, 10), TotalAmount = 30, OrderItems = new List<OrderItem>() },
                       new Order { OrderId = 5, ReservationId = 5, EmployeeId = 5, OrderDate = new DateOnly(2024, 11, 11), TotalAmount = 22, OrderItems = new List<OrderItem>() }
                   ];
        }
        public static OrderItem[] LoadOrderItems()
        {
            return [
                       new OrderItem { OrderItemId = 1, OrderId = 1, ItemId = 1, Quantity = 2 },
                       new OrderItem { OrderItemId = 2, OrderId = 1, ItemId = 2, Quantity = 1 },
                       new OrderItem { OrderItemId = 3, OrderId = 2, ItemId = 3, Quantity = 1 },
                       new OrderItem { OrderItemId = 4, OrderId = 2, ItemId = 4, Quantity = 1 },
                       new OrderItem { OrderItemId = 5, OrderId = 3, ItemId = 5, Quantity = 2 }
                   ];
        }
        public static Reservation[] LoadReservations()
        {
            return [
                       new Reservation { ReservationId = 1, CustomerId = 1, RestaurantId = 1, TableId = 1, ReservationDate = new DateOnly(2024, 11, 8), PartySize = 4, Orders = new List<Order>() },
                       new Reservation { ReservationId = 2, CustomerId = 2, RestaurantId = 1, TableId = 2, ReservationDate = new DateOnly(2024, 11, 9), PartySize = 2, Orders = new List<Order>() },
                       new Reservation { ReservationId = 3, CustomerId = 3, RestaurantId = 2, TableId = 3, ReservationDate = new DateOnly(2024, 11, 10), PartySize = 3, Orders = new List<Order>() },
                       new Reservation { ReservationId = 4, CustomerId = 4, RestaurantId = 2, TableId = 4, ReservationDate = new DateOnly(2024, 11, 11), PartySize = 5, Orders = new List<Order>() },
                       new Reservation { ReservationId = 5, CustomerId = 5, RestaurantId = 3, TableId = 5, ReservationDate = new DateOnly(2024, 11, 12), PartySize = 6, Orders = new List<Order>() }
                   ];
        }
        public static Restaurant[] LoadRestaurants()
        {
            return [
                       new Restaurant { RestaurantId = 1, Name = "Burger Haven", Address = "123 Burger St.", PhoneNumber = "555-1111", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) },
                       new Restaurant { RestaurantId = 2, Name = "Pasta Palace", Address = "456 Pasta Rd.", PhoneNumber = "555-2222", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) },
                       new Restaurant { RestaurantId = 3, Name = "Seafood Bistro", Address = "789 Seafood Blvd.", PhoneNumber = "555-3333", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) },
                       new Restaurant { RestaurantId = 4, Name = "Steakhouse Grill", Address = "321 Steak Dr.", PhoneNumber = "555-4444", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) },
                       new Restaurant { RestaurantId = 5, Name = "Vegan Delight", Address = "654 Green Ln.", PhoneNumber = "555-5555", Tables = new List<Table>(), Reservations = new List<Reservation>(), Employees = new List<Employee>(), MenuItems = new List<MenuItem>(), OpeningHours = new TimeSpan(8, 0, 0) }
                   ];
        }
        public static Table[] LoadTables()
        {
            return [
                       new Table { TableId = 1, RestaurantId = 1, Capacity = 4, Reservations = new List<Reservation>() },
                       new Table { TableId = 2, RestaurantId = 1, Capacity = 2, Reservations = new List<Reservation>() },
                       new Table { TableId = 3, RestaurantId = 2, Capacity = 4, Reservations = new List<Reservation>() },
                       new Table { TableId = 4, RestaurantId = 2, Capacity = 6, Reservations = new List<Reservation>() },
                       new Table { TableId = 5, RestaurantId = 3, Capacity = 8, Reservations = new List<Reservation>() }
                   ];
        }
        public static ReservationStatus[] LoadReservationsStatus()
        {
            return [
                     new ReservationStatus{ReservationStatusId = 1, ReservationId = 1, Status = ReservationStatusType.NotAttended, StatusDate = new DateOnly(2024, 11, 12) },
                     new ReservationStatus{ReservationStatusId = 2, ReservationId = 2, Status = ReservationStatusType.Completed, StatusDate = new DateOnly(2024, 11, 10) },
                     new ReservationStatus{ReservationStatusId = 3, ReservationId = 3, Status = ReservationStatusType.Cancelled, StatusDate = new DateOnly(2024, 11, 9) },
                     new ReservationStatus{ReservationStatusId = 4, ReservationId = 4, Status = ReservationStatusType.Confirmed, StatusDate = new DateOnly(2024, 11, 11) },
                     new ReservationStatus{ReservationStatusId = 5, ReservationId = 5, Status = ReservationStatusType.Pending, StatusDate = new DateOnly(2024, 11, 12) }
                   ];
        }
        public static async Task AddSeedData(RestaurantReservationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if(!await context.Employees.AnyAsync())
            {
                await context.Set<Employee>().AddRangeAsync(LoadEmployees());
            }

            if (!await context.MenuItems.AnyAsync())
            {
                await context.Set<MenuItem>().AddRangeAsync(LoadMenuItems());
            }

            if (!await context.Orders.AnyAsync())
            {
                await context.Set<Order>().AddRangeAsync(LoadOrders());
            }

            if (!await context.OrderItems.AnyAsync())
            {
                await context.Set<OrderItem>().AddRangeAsync(LoadOrderItems());
            }

            if (!await context.Restaurants.AnyAsync())
            {
                await context.Set<Restaurant>().AddRangeAsync(LoadRestaurants());
            }

            if (!await context.Reservations.AnyAsync())
            {
                await context.Set<Reservation>().AddRangeAsync(LoadReservations());
            }

            if (!await context.Customers.AnyAsync())
            {
                await context.Set<Customer>().AddRangeAsync(LoadCustomers());
            }

            if (!await context.Tables.AnyAsync())
            {
                await context.Set<Table>().AddRangeAsync(LoadTables());
            }

            if (!await context.ReservationsStatus.AnyAsync())
            {
                await context.Set<ReservationStatus>().AddRangeAsync(LoadReservationsStatus());
            }

            if (!await context.Users.AnyAsync())
            {
                await context.Set<User>().AddRangeAsync(LoadUsers());
            }

            await context.SaveChangesAsync();
        }

        public static User[] LoadUsers()
        {
            return
            [
                  new User
                  {
                      Id = 1,
                      UserName = "john_doe",
                      Email = "john@example.com",
                      Password = "Password123",
                      Role = UserRole.Customer
                  },
                  new User
                  {
                      Id = 2,
                      UserName = "sarah_smith",
                      Email = "sarah@example.com",
                      Password = "Password123",
                      Role = UserRole.Employee
                  },
                  new User
                  {
                      Id = 3,
                      UserName = "michael_jones",
                      Email = "michael@example.com",
                      Password = "Password123",
                      Role = UserRole.ResturantBoss
                  },
                  new User
                  {
                      Id = 4,
                      UserName = "alice_johnson",
                      Email = "alice@example.com",
                      Password = "Password123",
                      Role = UserRole.Admin
                  },
                  new User
                  {
                      Id = 5,
                      UserName = "david_white",
                      Email = "david@example.com",
                      Password = "Password123",
                      Role = UserRole.Customer
                  }
            ];
        }
    }
}
