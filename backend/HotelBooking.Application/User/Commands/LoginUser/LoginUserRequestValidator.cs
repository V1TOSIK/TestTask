using FluentValidation;

namespace HotelBooking.Application.User.Commands.LoginUser
{
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(x => x.Credential)
                .NotEmpty().WithMessage("Credential is required.")
                .EmailAddress().WithMessage("Credential must be a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
