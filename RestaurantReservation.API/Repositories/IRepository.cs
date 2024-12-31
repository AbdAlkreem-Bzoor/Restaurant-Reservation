namespace RestaurantReservation.API.Repositories
{
    public interface IRepository 
    {
        Task<bool> SaveChangesAsync();
    }
}
