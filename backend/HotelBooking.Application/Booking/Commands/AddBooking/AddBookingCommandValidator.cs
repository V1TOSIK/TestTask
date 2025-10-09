using FluentValidation;

namespace HotelBooking.Application.Booking.Commands.AddBooking
{
    public class AddBookingCommandValidator : AbstractValidator<AddBookingCommand>
    {
        public AddBookingCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotEmpty().WithMessage("At least one booking request is required.")
                .SetValidator(new AddBookingRequestValidator());
        }
    }
}
