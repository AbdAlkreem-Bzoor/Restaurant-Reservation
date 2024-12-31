using FluentValidation;
using RestaurantReservation.API.Models.Customer;

namespace RestaurantReservation.API.Validators.Customer
{
    public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateDtoValidator()
        {

            RuleFor(x => x.FirstName).ValidName();

            RuleFor(x => x.LastName).ValidName();

            RuleFor(x => x.Email).NotNull()
                                 .NotEmpty()
                                 .MaximumLength(255)
                                 .EmailAddress();

            RuleFor(x => x.PhoneNumber).ValidPhoneNumber();
        }
    }
}
