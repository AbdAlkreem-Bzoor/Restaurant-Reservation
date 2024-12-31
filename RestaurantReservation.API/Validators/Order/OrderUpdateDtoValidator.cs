using FluentValidation;
using RestaurantReservation.API.Models.Order;

namespace RestaurantReservation.API.Validators.Order
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(x => x.EmployeeId).NotNull()
                                       .NotEmpty()
                                       .InclusiveBetween(1, 1_000_000_000);
            RuleFor(x => x.ReservationId).NotNull()
                                         .NotEmpty()
                                         .InclusiveBetween(1, 1_000_000_000);

            RuleFor(x => x.TotalAmount).NotNull()
                                       .NotEmpty()
                                       .GreaterThanOrEqualTo(0);
        }
    }
}
