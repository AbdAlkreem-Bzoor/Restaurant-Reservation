using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface ITableRepository : IRepository
    {
        Task<IEnumerable<Table>> GetTablesAsync();
        Task<Table?> GetTableAsync(int id);
        Task<bool> AddTableAsync(Table table);
        Task<bool> UpdateTableAsync(Table table);
        Task<bool> DeleteTableAsync(int id);
    }
}
