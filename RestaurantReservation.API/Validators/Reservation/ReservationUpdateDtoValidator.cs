using FluentValidation;
using RestaurantReservation.API.Models.Reservation;

namespace RestaurantReservation.API.Validators.Reservation
{
    public class ReservationUpdateDtoValidator : AbstractValidator<ReservationUpdateDto>
    {
        public ReservationUpdateDtoValidator()
        {
            RuleFor(x => x.RestaurantId).NotNull()
                                        .NotEmpty()
                                        .InclusiveBetween(1, 1_000_000_000);
            RuleFor(x => x.CustomerId).NotNull()
                                      .NotEmpty()
                                      .InclusiveBetween(1, 1_000_000_000);
            RuleFor(x => x.TableId).NotNull()
                                   .NotEmpty()
                                   .InclusiveBetween(1, 1_000_000_000);
            RuleFor(x => (int)x.PartySize).NotNull()
                                          .NotEmpty()
                                          .InclusiveBetween(1, 10);
        }
    }
}
