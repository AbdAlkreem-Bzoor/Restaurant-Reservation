using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
        Task<Restaurant?> GetRestaurantAsync(int id);
        Task<bool> AddRestaurantAsync(Restaurant restaurant);
        Task<bool> UpdateRestaurantAsync(Restaurant restaurant);
        Task<bool> DeleteRestaurantAsync(int id);
    }
}
