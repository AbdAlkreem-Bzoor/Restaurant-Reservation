namespace RestaurantReservation.API.Models
{
    public enum UserRole
    {
        Customer,
        Employee,
        ResturantBoss,
        Admin
    }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
