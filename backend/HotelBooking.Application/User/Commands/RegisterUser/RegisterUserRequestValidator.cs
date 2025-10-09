using FluentValidation;

namespace HotelBooking.Application.User.Commands.RegisterUser
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Credential)
                .NotEmpty().WithMessage("Credential is required.")
                .Must(c => c.Contains("@") || c.All(char.IsDigit)).WithMessage("Credential must be a valid email or phone number.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
