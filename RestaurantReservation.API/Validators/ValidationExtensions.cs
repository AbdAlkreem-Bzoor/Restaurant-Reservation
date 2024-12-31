using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace RestaurantReservation.API.Validators
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidName<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotNull()
                       .NotEmpty()
                       .Matches(@"^[A-Za-z\s]+$")
                       .WithMessage(ValidationErrorMessages.ValidName)
                       .Length(3, 30);
        }

        public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotNull()
                       .NotEmpty()
                       .MinimumLength(8)
                       .MaximumLength(30)
                       .Matches("[A-Z]")
                       .WithMessage(ValidationErrorMessages.PasswordUpperCaseCharacters)
                       .Matches("[a-z]")
                       .WithMessage(ValidationErrorMessages.PasswordLowerCaseCharacters)
                       .Matches("[0-9]")
                       .WithMessage(ValidationErrorMessages.PasswordDigits)
                       .Matches("[^a-zA-Z0-9]")
                       .WithMessage(ValidationErrorMessages.PasswordSpecialCharacters);
        }

        public static IRuleBuilderOptions<T, string> ValidPhoneNumber<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotNull()
                       .NotEmpty()
                       .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
                       .WithMessage(ValidationErrorMessages.ValidPhoneNumber)
                       .Length(15);
        }
    }
}
