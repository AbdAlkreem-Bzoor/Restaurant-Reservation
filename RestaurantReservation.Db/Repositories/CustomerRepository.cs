using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using System.Data;
using System.Drawing;

namespace RestaurantReservation.Db.Repositories
{
    public class CustomerRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public CustomerRepository(RestaurantReservationDbContext context)
        {
            _context = context;
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
        public async Task<List<CustomerWithPartySizeAbove>> GetCustomersWithLargePartySizeAsync(int partySize)
        {
            //var parameter = new SqlParameter("@Size", SqlDbType.Int)
            //{
            //    Value = partySize
            //};
            return await _context.CustomersWithPartySizeAbove.FromSql($"EXEC dbo.sp_CustomersWithPartySizeAbove {partySize}").ToListAsync();
        }
    }
}
