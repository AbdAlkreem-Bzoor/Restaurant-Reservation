using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface ICustomerRepository : IRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerAsync(int id);
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<bool> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<bool> IsCustomerExistByEmail(string email);
    }
}
