using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(int id);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<int> GetUserId(string userName, string password);
        Task<bool> IsUserExistByUserName(string userName);
        Task<User?> AuthenticateAsync(string userName, string password);
    }
}
