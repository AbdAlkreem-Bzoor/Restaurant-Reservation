using FluentValidation;
using RestaurantReservation.API.Models.Customer;

namespace RestaurantReservation.API.Validators.Customer
{
    public class CustomerCreationDtoValidator : AbstractValidator<CustomerCreationDto>
    {
        public CustomerCreationDtoValidator()
        {
            RuleFor(x => x.FirstName).ValidName();

            RuleFor(x => x.LastName).ValidName();

            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.PhoneNumber).ValidPhoneNumber();
        }
    }
}
