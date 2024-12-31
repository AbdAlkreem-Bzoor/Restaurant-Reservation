using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface IOrderItemRepository : IRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync();
        Task<OrderItem?> GetOrderItemAsync(int id);
        Task<bool> AddOrderItemAsync(OrderItem orderItem);
        Task<bool> UpdateOrderItemAsync(OrderItem orderItem);
        Task<bool> DeleteOrderItemAsync(int id);
    }
}
