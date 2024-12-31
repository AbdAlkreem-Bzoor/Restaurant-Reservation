using FluentValidation;
using RestaurantReservation.API.Models.User;

namespace RestaurantReservation.API.Validators.Authentication
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.UserName).ValidName();

            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).StrongPassword();
        }
    }
}
