using FluentValidation;
using RestaurantReservation.API.Models.Order;

namespace RestaurantReservation.API.Validators.Order
{
    public class OrderCreationDtoValidator : AbstractValidator<OrderCreationDto>
    {
        public OrderCreationDtoValidator()
        {
            RuleFor(x => x.EmployeeId).NotNull()
                                      .NotEmpty()
                                      .InclusiveBetween(1, 1_000_000_000);
            RuleFor(x => x.OrderDate).NotNull().
                                     NotEmpty();
            RuleFor(x => x.ReservationId).NotNull()
                                         .NotEmpty()
                                         .InclusiveBetween(1, 1_000_000_000);

            RuleFor(x => x.TotalAmount).NotNull()
                                       .NotEmpty()
                                       .GreaterThanOrEqualTo(0);
        }
    }
}
