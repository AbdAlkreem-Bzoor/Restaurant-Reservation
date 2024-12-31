using FluentValidation;
using RestaurantReservation.API.Models.OrderItem;

namespace RestaurantReservation.API.Validators.OrderItem
{
    public class OrderItemCreationDtoValidator : AbstractValidator<OrderItemCreationDto>
    {
        public OrderItemCreationDtoValidator()
        {
            RuleFor(x => x.ItemId).NotEmpty();

            RuleFor(x => x.Quantity).NotNull()
                                    .NotEmpty()
                                    .GreaterThan(0);
        }
    }
}
