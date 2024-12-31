using FluentValidation;
using RestaurantReservation.API.Models.Table;

namespace RestaurantReservation.API.Validators.Table
{
    public class TableCreationDtoValidator : AbstractValidator<TableCreationDto>
    {
        public TableCreationDtoValidator()
        {
            RuleFor(x => x.RestaurantId).NotNull()
                                        .NotEmpty()
                                        .InclusiveBetween(1, 1_000_000_000);

            RuleFor(x => x.Capacity).NotNull()
                                    .NotEmpty()
                                    .InclusiveBetween(1, 10);
        }
    }
}