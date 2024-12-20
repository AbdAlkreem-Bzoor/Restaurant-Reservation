using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetMenuItemsAsync();
        Task<MenuItem?> GetMenuItemAsync(int id);
        Task<bool> AddMenuItemAsync(MenuItem menuItem);
        Task<bool> UpdateMenuItemAsync(MenuItem menuItem);
        Task<bool> DeleteMenuItemAsync(int id);
    }
}
