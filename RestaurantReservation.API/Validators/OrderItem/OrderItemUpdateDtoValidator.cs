using FluentValidation;
using RestaurantReservation.API.Models.OrderItem;

namespace RestaurantReservation.API.Validators.OrderItem
{
    public class OrderItemUpdateDtoValidator : AbstractValidator<OrderItemUpdateDto>
    {
        public OrderItemUpdateDtoValidator()
        {
            RuleFor(x => x.Quantity).NotNull()
                                    .NotEmpty()
                                    .GreaterThan(0);
        }
    }
}
