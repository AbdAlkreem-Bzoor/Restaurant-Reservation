using FluentValidation;
using RestaurantReservation.API.Models.User;

namespace RestaurantReservation.API.Validators.Authentication
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator() 
        {
            RuleFor(x => x.UserName).ValidName();

            RuleFor(x => x.Password).StrongPassword();
        }
    }
}
