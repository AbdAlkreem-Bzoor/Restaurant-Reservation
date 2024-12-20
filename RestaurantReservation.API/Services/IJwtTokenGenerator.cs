namespace RestaurantReservation.API.Services
{
    public interface IJwtTokenGenerator
    {
        Task<string?> GenerateToken(int id);
        bool ValidateToken(string token);
    }
}
