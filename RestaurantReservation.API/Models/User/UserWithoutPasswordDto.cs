namespace RestaurantReservation.API.Models.User
{
    public class UserWithoutPasswordDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
