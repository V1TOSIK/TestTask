using FluentValidation;

namespace HotelBooking.Application.User.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotNull().WithMessage("Register data cannot be empty")
                .SetValidator(new RegisterUserRequestValidator());
        }
    }
}
