using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Repositories
{
    public interface IApplicationRepository : ICustomerRepository, 
                                                        IEmployeeRepository, 
                                                        IMenuItemRepository,
                                                        IOrderRepository,
                                                        IOrderItemRepository,
                                                        IReservationRepository,
                                                        IRestaurantRepository,
                                                        ITableRepository,
                                                        IUserRepository
    {

    }
}
