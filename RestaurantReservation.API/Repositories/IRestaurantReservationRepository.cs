using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface IRestaurantReservationRepository : ICustomerRepository, 
                                                        IEmployeeRepository, 
                                                        IMenuItemRepository,
                                                        IOrderRepository,
                                                        IOrderItemRepository,
                                                        IReservationRepository,
                                                        IRestaurantRepository,
                                                        ITableRepository,
                                                        IUserRepository
    {
        Task<bool> SaveChangesAsync();
    }
}
