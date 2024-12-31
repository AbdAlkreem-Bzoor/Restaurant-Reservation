using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface IReservationRepository : IRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Reservation?> GetReservationAsync(int id);
        Task<bool> AddReservationAsync(Reservation reservation);
        Task<bool> UpdateReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsForCustomerAsync(int customerId);
        Task<IEnumerable<Order>> GetOrdersForReservationAsync(int reservationId);
        Task<IEnumerable<MenuItem>> GetMenuItemsForReservationAsync(int reservationId);
        Task<IEnumerable<KeyValuePair<Order, List<MenuItem>>>> GetOrdersAndMenuItemForReservationAsync(int reservationId);
    }
}
