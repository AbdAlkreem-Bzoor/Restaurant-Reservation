using RestaurantReservation.API.Models.User;

namespace RestaurantReservation.API.Services
{
    public interface IJwtTokenGenerator
    {
        Task<string?> GenerateToken(int id);
        string? GenerateToken(UserWithoutPasswordDto user);
        bool ValidateToken(string token);
    }
}
