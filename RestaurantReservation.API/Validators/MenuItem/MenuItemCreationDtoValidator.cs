using FluentValidation;
using RestaurantReservation.API.Models.MenuItem;

namespace RestaurantReservation.API.Validators.MenuItem
{
    public class MenuItemCreationDtoValidator : AbstractValidator<MenuItemCreationDto>
    {
        public MenuItemCreationDtoValidator()
        {
            RuleFor(x => x.Name).ValidName();

            RuleFor(x => x.Price).NotNull()
                                 .NotEmpty()
                                 .GreaterThan(0);

            RuleFor(x => x.RestaurantId).NotNull()
                                        .NotEmpty()
                                        .InclusiveBetween(1, 1_000_000_000);
        }
    }
}
