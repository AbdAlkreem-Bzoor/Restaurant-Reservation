using FluentValidation;
using RestaurantReservation.API.Models.Restaurant;

namespace RestaurantReservation.API.Validators.Restaurant
{
    public class RestaurantCreationDtoValidator : AbstractValidator<RestaurantCreationDto>
    {
        public RestaurantCreationDtoValidator()
        {
            RuleFor(x => x.Address).NotNull()
                                   .NotEmpty();

            RuleFor(x => x.Name).ValidName();

            RuleFor(x => x.OpeningHours).NotNull().NotEmpty();

            RuleFor(x => x.PhoneNumber ?? string.Empty).ValidPhoneNumber();
        }
    }
    public class RestaurantUpdateDtoValidator : AbstractValidator<RestaurantUpdateDto>
    {
        public RestaurantUpdateDtoValidator()
        {
            RuleFor(x => x.Address).NotNull()
                                  .NotEmpty();

            RuleFor(x => x.Name).ValidName();

            RuleFor(x => x.OpeningHours).NotNull().NotEmpty();

            RuleFor(x => x.PhoneNumber ?? string.Empty).ValidPhoneNumber();
        }
    }
}
