using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (var context = new RestaurantReservationDbContext())
            {
                //var result = await new EmployeeRepository(context).ListManagersAsync();
                //foreach (var item in result)
                //{
                //    Console.WriteLine(item);
                //}

                //var result = await new ReservationRepository(context).GetReservationsByCustomerAsync(1);
                //foreach (var item in result)
                //{
                //    Console.WriteLine(item);
                //}

                //var result = await new OrderRepository(context).ListOrdersAndMenuItemsAsync(1);
                //foreach (var item in result)
                //{
                //    Console.WriteLine(item.Key);
                //    foreach (var r in item.Value)
                //    {
                //        Console.WriteLine(r?.ToString()?.PadLeft(50, ' '));
                //    }
                //}

                //var result = await new MenuItemRepository(context).ListOrderedMenuItemsAsync(1);
                //foreach (var item in result)
                //{
                //    Console.WriteLine(item);
                //}

                //Console.WriteLine(await new OrderRepository(context).CalculateAverageOrderAmountAsync(2));

                //var result = await new CustomerRepository(context).GetCustomersWithLargePartySizeAsync(6);

                //foreach (var item in result)
                //{
                //    Console.WriteLine(item);
                //}

                //var result = context.ReservationsCustomerRestaurantDetails.ToList();

                //foreach (var item in result) Console.WriteLine(item);

                //var result = context.EmployeeRestaurantDetails.ToList();

                //foreach (var item in result) Console.WriteLine(item + "\n\n\n");


                //var result = context.Restaurants.Select(r => new
                //{
                //    r.Name,
                //    Revenue = RestaurantReservationDbContext.CalculateTotalRevenueByRestaurant(r.RestaurantId)
                //}).ToList();

                //foreach (var item in result)
                //{
                //    Console.WriteLine($"Restaurant Name : {item.Name}  |  Revenue = {item.Revenue}");
                //}

                //var result = await new CustomerRepository(context).GetCustomersWithLargePartySizeAsync(1);

                //foreach (var item in result) Console.WriteLine(item);

                //await new CustomerRepository(context).CreateCustomerAsync(new Customer { FirstName = "A", LastName = "Z", Email = "asdf@example.com"});
                //await new CustomerRepository(context).UpdateCustomerAsync(new Customer { CustomerId = 6, FirstName = "ABC", LastName = "ZYX", Email = "asdf@example.com" });
                //await new CustomerRepository(context).DeleteCustomerAsync(6);

                //await context.Tables.AddAsync(new Table { Capacity = 10, RestaurantId = 1 });
                //await context.SaveChangesAsync();
                //Console.ReadKey();
                //var result = await new TableRepository(context).GetAvailableTablesAsync();
                //foreach (var item in result) Console.WriteLine(item);


            }

            Console.ReadKey();
        }
    }
}


