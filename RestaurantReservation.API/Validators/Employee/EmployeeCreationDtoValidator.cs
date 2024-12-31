using FluentValidation;
using RestaurantReservation.API.Models.Employee;

namespace RestaurantReservation.API.Validators.Employee
{
    public class EmployeeCreationDtoValidator : AbstractValidator<EmployeeCreationDto>
    {
        public EmployeeCreationDtoValidator()
        {
            RuleFor(x => x.RestaurantId).NotNull()
                                        .NotEmpty()
                                        .InclusiveBetween(1, 1_000_000_000);

            RuleFor(x => x.FirstName).ValidName();

            RuleFor(x => x.LastName).ValidName();

            RuleFor(x => x.Position).NotNull()
                                    .NotEmpty();
        }
    }
}
