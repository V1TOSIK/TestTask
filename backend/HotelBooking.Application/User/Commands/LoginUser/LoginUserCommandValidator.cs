using FluentValidation;

namespace HotelBooking.Application.User.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Request).NotNull().WithMessage("LoginUserRequest cannot be null.")
                .SetValidator(new LoginUserRequestValidator());
        }
    }
}
